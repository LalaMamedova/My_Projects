using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestsApp.Repository.ModelReposotry;
using TestsLib.Dto;
using TestsLib.Models;

namespace TestAPİ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly TestRepository _testRepository;
    public TestController(TestRepository repository)
    {
        _testRepository = repository;
    }

    [HttpPost("Post")]
    public async Task<IActionResult> Create([FromBody] TestDto test)
    {
        try
        {
            await _testRepository.CreateAsync(test);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);  
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _testRepository.GetAll());
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetAll(string id)
    {
        return Ok(await _testRepository.GetById(id));
    }

    [HttpGet("GetPopularTags")]
    public async Task<IActionResult> GetPopularTags()
    {
        return Ok(await _testRepository.GetMostPopularTags());
    }
    [HttpGet("GetTestsByTag/{tag}")]
    public async Task<IActionResult> GetPopularTags(string tag)
    {
        return Ok(await _testRepository.GetAllTestsByTag(tag));
    }
    [HttpDelete("Delete/{id}")] 
    public async Task<IActionResult> Delete(string userId, string id)
    {
        await _testRepository.DeleteAsync(userId,id);
        return Ok();
    }
}
