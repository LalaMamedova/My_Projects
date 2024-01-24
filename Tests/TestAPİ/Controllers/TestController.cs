using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Repository.ModelReposotry;
using TestsLib.DbContexts;
using TestsLib.Models;

namespace TestAPİ.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly TestRepository _repository;

    public TestController(TestRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTest()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Test test)
    {
        await _repository.CreateAsync(test);
        return Ok();
    }
}
