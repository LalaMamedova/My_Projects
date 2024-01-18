using AutoMapper;
using Azure;
using DeviceApp.Cache;
using DeviceApp.Repo.Interface;
using DeviceApp.Services;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class ProductRepository: IRepository
{
    private readonly IGenericRepository<Product, ProductDto> _repository;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly PageList<Product> pageList;
    private readonly string redisKey = "products";
    public ProductRepository(IGenericRepository<Product, ProductDto> repository,
                             ICacheService cacheService, IMapper mapper)
    {
        _repository = repository;
        _cacheService = cacheService;
        _mapper = mapper;
        pageList = new();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {

        if (await _cacheService.IsExist(redisKey))
        {
            List<Product>? responseFromCache = await _cacheService.GetAsync<List<Product>>(redisKey);
            var originalEntity = _mapper.Map<List<Product>>(responseFromCache);
            return originalEntity;
        }

        var products = await _repository.GetAllAsync(null,
                                         soruce => soruce
                                         .Include(x => x.ProductsImg));

        var productDto = _mapper.Map<List<ProductDto>>(products);

        await _cacheService.SetAsync(redisKey, productDto);

        return products;
    }
    

    public async Task<Product?> GetFullProductById(int id)
    {
        var product = await _repository.GetFirstOrDefaultAsync(x => x.Id == id,
                     false,
                     soruce => soruce
                     .Include(x => x.ProductsImg)
                     .Include(x => x.OriginalImgs)
                     .Include(x => x.Brand)
                     .Include(x=>x.Reviews)
                     .Include(x=>x.SubCategory)
                     .Include(x => x.ProductСharacteristics)
                     .ThenInclude(x => x.Characteristic));

        return product;
    }

    public async Task<Product?> GetById(int id)
    {
        var product = await _repository.GetFirstOrDefaultAsync(x => x.Id == id,
                     false,
                     soruce => soruce
                     .Include(x => x.ProductsImg));

        return product;
    }

    public async Task<IEnumerable<Product>> GetRecomendation(int categoryId, int productId, IEnumerable<int> viewedProducts)
    {
        var allProducts = await _repository.GetAllAsync(filter: x => x.SubCategory.CategoryId == categoryId && x.Id != productId,
                                                        include: source => source.Include(x => x.ProductsImg));

        var notViewedProducts = allProducts.Where(x => !viewedProducts.Contains(x.Id));
        var pagedProduct = pageList.Get(notViewedProducts.ToList(), 1, 5);

        return pagedProduct;

    }

    public async Task AddAsync(ProductDto model)
    {
        await _repository.AddAsync(model);
        await _repository.SaveChangesAsync();
        var products = await _repository.GetAllAsync(null,
                                        soruce => soruce
                                        .Include(x => x.ProductsImg));

        var dtoProduct = _mapper.Map<ProductDto>(products);
        await _cacheService.SetAsync("products", dtoProduct);

    }


    public async Task<object>? GetByPage(int page, int takeCount)
    {
        var products = await GetAllAsync();
        
        var pagedProduct = pageList.Get(products.ToList(), page, takeCount);

        if (pagedProduct != null)
        {
            return new
            {
                Items = pagedProduct,
                TotalItemCount = pageList.TotalItems,
                CurrentPage = pageList.CurrentPage,
                TotalPages = pageList.TotalPages,
                TakeCount = takeCount
            };
        }
        else
        {
            throw new ArgumentNullException(nameof(products) + " is not found");
        }
    }

    public async Task<object>? GetWithCategoryId(int categoryId, int page, int takeCount)
    {
        var products = await _repository.GetAllAsync
                    (x => x.SubCategory!.CategoryId == categoryId,
                    soruce => soruce
                    .Include(x => x.ProductsImg)
                    .Include(x => x.SubCategory)
                    .Include(x => x.Reviews),

                    x => x.SubCategory!.Products = null);

        var pagedProduct = pageList.Get(products.ToList(), page, takeCount);

        if (pagedProduct != null)
        {
            return new
            {
                Items = pagedProduct,
                TotalItemCount = pageList.TotalItems,
                CurrentPage = pageList.CurrentPage,
                TotalPages = pageList.TotalPages,
                TakeCount = takeCount
            };
        }
        else
        {
            throw new ArgumentNullException(nameof(products) + " is not found");
        }


    }

    public async Task<object?> GetWithSubCategoryId(int subCategoryId, int page, int takeCount)
    {
        var products = await _repository.GetAllAsync
                   (x => x.SubCategory.Id == subCategoryId,
                   soruce => soruce
                   .Include(x => x.ProductsImg)
                   .Include(x => x.SubCategory)
                   .Include(x=>x.Reviews),
                   x => x.SubCategory.Products = null);

        var pagedProduct = pageList.Get(products.ToList(), page, takeCount);

        if (pagedProduct != null)
        {
            return new
            {
                Items = pagedProduct,
                TotalItemCount = pageList.TotalItems,
                CurrentPage = pageList.CurrentPage,
                TotalPages = pageList.TotalPages,
                TakeCount = takeCount
            };
        }
        else
        {
            throw new ArgumentNullException(nameof(products) + " is not found");
        }


    }

    public async Task<object?> GetNewProducts()
    {
        int takeCount = 6;

        var products = await _repository.GetAllAsync
                   (orderBy:x=>x.OrderBy(x=>x.AddedDate),
                   include: soruce => soruce
                   .Include(x => x.ProductsImg)
                   .Include(x => x.SubCategory)
                   .Include(x => x.Reviews),

                   additionalFilter: x => x.SubCategory.Products = null);

        var pagedProduct = pageList.Get(products.ToList(), 1, takeCount);

        if (pagedProduct != null)
        {
            return new
            {
                Items = pagedProduct,
                TotalItemCount = pageList.TotalItems,
                CurrentPage = pageList.CurrentPage,
                TotalPages = pageList.TotalPages,
                TakeCount = takeCount
            };
        }
        else
        {
            throw new ArgumentNullException(nameof(products) + " is not found");
        }
    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            await _repository.RemoveAsync(id,source =>source
            .Include(x=>x.ProductСharacteristics)
            .Include(x=>x.ProductsImg),
            x=>x.Id ==id);
            await _repository.SaveChangesAsync();

            await _cacheService.RemoveAsync("products");
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<Product> Update(ProductDto model, int id)
    {
        try
        {
            var entity = await _repository.Update(model, source => source.Include(x => x.ProductСharacteristics));
            await _repository.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(id + " is not found");
        }
    }
}

