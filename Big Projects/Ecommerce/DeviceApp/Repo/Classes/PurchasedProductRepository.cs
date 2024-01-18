using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using EcommerceLib.Models.UserModel.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeviceApp.Repo.Classes;

public class PurchasedProductRepository
{
    private readonly IRepository<PurchasedProduct, PurchasedProductDto> _repository;
    private readonly UserManager<AppUser> _userManager;
    public PurchasedProductRepository(IRepository<PurchasedProduct, PurchasedProductDto> repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }
    public async Task<ICollection<PurchasedProduct>?> BuyAsync(IEnumerable<PurchasedProductDto> purchasedProducts, 
                                          string userId)
    {
        if (purchasedProducts.Count() > 0)
        {
            foreach (var purchasedProduct in purchasedProducts)
            {
                await _repository.AddAsync(purchasedProduct);
            }

            await _repository.SaveChangesAsync();

        }

        var usersPurchasedProducts = await _userManager.Users
             .Include(x => x.PurchasedProducts)
             .FirstOrDefaultAsync(x => x.Id == userId);

        return usersPurchasedProducts.PurchasedProducts;
    }
    public async Task<ICollection<PurchasedProduct>?> GetAllAsync( string userId)
    {
    
        var usersPurchasedProducts = await _userManager.Users
             .Include(x => x.PurchasedProducts)
             .ThenInclude(x=>x.Product)
             .ThenInclude(x=>x.ProductsImg)
             .FirstOrDefaultAsync(x => x.Id == userId);

        return usersPurchasedProducts.PurchasedProducts;
    }
}
