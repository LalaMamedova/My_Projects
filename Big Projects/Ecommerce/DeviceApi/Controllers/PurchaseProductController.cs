using DeviceApp.Repo.Classes;
using EcommerceLib.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DeviceApi.Controllers;
[Route("api/v1/[controller]")]
[ApiController()]
public class PurchaseProductController:ControllerBase
{
    private readonly PurchasedProductRepository _purchaseProductRepository;

    public PurchaseProductController(PurchasedProductRepository purchaseProductRepository)
    {
        _purchaseProductRepository = purchaseProductRepository;
    }

    [HttpPost("{userId}")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] IEnumerable<PurchasedProductDto> purchasedProducts, string userId)
    {
        var response = await _purchaseProductRepository.BuyAsync(purchasedProducts, userId);
        return Ok(response);    
    }

    [HttpPost("GetAll/{userId}")]
    [Authorize]
    public async Task<IActionResult> GetAll(string userId)
    {
        var response = await _purchaseProductRepository.GetAllAsync( userId);
        return Ok(response);
    }
}
