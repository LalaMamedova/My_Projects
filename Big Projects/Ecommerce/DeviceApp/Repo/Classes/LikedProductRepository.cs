using AutoMapper;
using DeviceApp.Cache;
using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class LikedProductRepository
{
    readonly IRepository<UserLikedProduct, UserLikedProductDto> _repository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public LikedProductRepository(IRepository<UserLikedProduct, UserLikedProductDto> repository, UserManager<AppUser> userManager, ICacheService cacheService, IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserLikedProduct>?> GetAllLikedProductsAsync(string userId)
    {
        if (await _cacheService.IsExist($"{userId} likedProducts"))
        {
            List<UserLikedProduct>? responseFromCache = await _cacheService
                                              .GetAsync<List<UserLikedProduct>>($"{userId} likedProducts");

            var originalEntity = _mapper.Map<List<UserLikedProduct>>(responseFromCache);
            return originalEntity;
        }

        var userInDb = await _userManager.Users
           .Include(x => x.FavoriteProducts)
           .FirstOrDefaultAsync(x => x.Id == userId);

        var favoriteProductsDto = _mapper.Map<List<UserLikedProductDto>>(userInDb.FavoriteProducts);
        await _cacheService.SetAsync($"{userId} likedProducts", favoriteProductsDto);

        return userInDb.FavoriteProducts;
    }
    public async Task<UserLikedProduct?> AddAsync(UserLikedProductDto userProduct)
    {
        var repeatRateCheck = await _repository
            .GetAllAsync(filter: x => x.AppUserId == userProduct.AppUserId && x.ProductId == userProduct.ProductId);

        if (repeatRateCheck == null || repeatRateCheck.Count() == 0)
        {
            await _repository.AddAsync(userProduct);
            await _repository.SaveChangesAsync();

            var userInDb = await _userManager.Users
                .Include(x => x.FavoriteProducts)
                .FirstOrDefaultAsync(x => x.Id == userProduct.AppUserId);

            return userInDb.FavoriteProducts
                .Where(x => x.ProductId == userProduct.ProductId && x.AppUserId == userProduct.AppUserId)
                .LastOrDefault();
        }
        else
        {
            return default;
        }


    }
    public async Task RemoveAsync(int id, string userId)
    {
        var repeatRateCheck = await _repository
            .GetAllAsync(filter: x => x.Id == id);

        if (repeatRateCheck.Count() > 0)
        {
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();

            var userInDb = await _userManager.Users
              .Include(x => x.FavoriteProducts)
              .FirstOrDefaultAsync(x => x.Id == userId);

            if (userInDb.FavoriteProducts.Count > 0)
            {
                var favoriteProductsDto = _mapper.Map<List<UserLikedProductDto>>(userInDb.FavoriteProducts);
                await _cacheService.SetAsync($"{userId} likedProducts", favoriteProductsDto);
            }
            else
            {
                await _cacheService.RemoveAsync($"{userId} likedProducts");
            }

        }
        else
        {
            throw new NullReferenceException();
        }

    }

}
