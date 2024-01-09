using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class CategoryRepository
{
    private readonly IRepository<Category, CategoryDto> _repository;
    public CategoryRepository(IRepository<Category, CategoryDto> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(CategoryDto model)
    {
        await _repository.AddAsync(model);
        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync(null, source => source.Include(x => x.SubCategories));
        return categories;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        try
        {
            var category = await _repository.GetFirstOrDefaultAsync(x => x.Id == id, false,
                 source =>
                 source
                 .Include(x => x.SubCategories)
                 .ThenInclude(x => x.Characteristics));

            return category;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(id + " is not found");
        }

    }

    public async Task<Category> GetByNameAsync(string name)
    {
        try
        {
            var categories = await _repository.GetFirstOrDefaultAsync(
                                    filter: x => x.Name == name);
            return categories;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(name + " is not found");
        }
    }

    public async Task RemoveAsync(int id)
    {
        try
        {
            await _repository
                .RemoveAsync(id,include:x=>x
                .Include(x=>x.SubCategories)
                .ThenInclude(x=>x.Products)
                .ThenInclude(x=>x.ProductСharacteristics),
                filter:x=>x.Id == id);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }
    public async Task<Category> UpdateAsync(CategoryDto model)
    {
        try
        {
            var entity = await _repository.Update(model);
            await _repository.SaveChangesAsync();
            return entity;

        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);

        }
    }

}


