
using DeviceApp.Repo.Classes;
using DeviceApp.Repo.Interface;
using EcommerceLib.DTO;
using EcommerceLib.Models.ProductModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceServer.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class SubCategoryController : ControllerBase
{
   
    private SubCategoryRepository _repository;
    public SubCategoryController(SubCategoryRepository subCategoryRepository)
    {
        _repository = subCategoryRepository;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(SubCategoryDto model)
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
            var subcategory = await _repository.GetByIdAsync(id);
            return Ok(subcategory);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message) ;
        }
       
    }

    [HttpGet("Get/{name:alpha}")]
    public async Task<IActionResult> GetByName(string name)
    {
        try
        {
            var subcategory = await _repository.GetByNameAsync(name);
            return Ok(subcategory);
        }
        catch (Exception ex)
        {
            return NotFound(new { Description = ex.Message });
        }
    }

    [HttpDelete("Delete/{id:int}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _repository.RemoveAsync(id);
            return Ok((new { Description = "Deleted" }));
        }
        catch (Exception ex)
        {
            return NotFound(new { Description = ex.Message });
        }

    }



    [HttpPut("Update")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(SubCategoryDto model)
    {
        try
        {
            var entity = await _repository.UpdateAsync(model);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return NotFound(new { Description = ex.Message });
        }
    }
}
