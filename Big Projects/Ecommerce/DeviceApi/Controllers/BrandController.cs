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
public class BrandController:ControllerBase
{

    private BrandRepository _repository;
    public BrandController(BrandRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(BrandDto model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _repository.AddAsync(model);
                return Ok(new { Description = "Succeed" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Description = ex.Message });
            }
        }
        return BadRequest(new { Description = "Some field are empty" });
    }


    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var brand = await _repository.GetAllAsync();
            return Ok(brand);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var brand = await _repository.GetByIdAsync(id);

            return Ok(brand);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("Delete/{id:int}")]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.RemoveAsync(id);
            return Ok();

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }


    [HttpPut("Update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(BrandDto model)
    {
        try
        {
            var entity = await _repository.UpdateAsync(model);
            return Ok(entity);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
