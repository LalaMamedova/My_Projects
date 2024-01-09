using DeviceApp.Repo.Classes;
using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceServer.Controllers;

[Route("api/v1/[controller]")]
[ApiController()]
public class CategoryController:ControllerBase
{
    private CategoryRepository _repository;
    public CategoryController(CategoryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(CategoryDto model)
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


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var subcategories = await _repository.GetAllAsync();
            return Ok(subcategories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
     
    }

    [HttpGet("Get/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var category = await _repository.GetByIdAsync(id);

            return Ok(category);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("Get/{name}")]
    public async Task<IActionResult> Get(string name)
    {
        try
        {
            var category = await _repository.GetByNameAsync(name);
            return Ok(category);
        }
        catch (Exception ex)
        {

            return NotFound(ex.Message);
        }
    }

    [HttpDelete("Delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.RemoveAsync(id);
            return Ok("Deleted");

        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }


    [HttpPut("Update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] CategoryDto model)
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
