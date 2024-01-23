using AutoMapper;
using DeviceApp.Repo.Classes;
using DeviceApp.Repo.Interface;
using EcommerceLib.Context;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceServer.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ProductСharacteristicController : ControllerBase
{

    private ProductСharacteristicRepository _repository;
    public ProductСharacteristicController(ProductСharacteristicRepository repository)
    {
        _repository = repository;
    }

   
    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetValueById(int id)
    {
        try
        {
            var values = await _repository.GetCharacteristicValuesById(id);
            return Ok(values);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    

}
