using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Repository.ModelRepository;
using QuizLib.Model;

namespace QuizAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuizController : ControllerBase
{
    private QuizRepository _quizRepository;
    public QuizController(QuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _quizRepository.GetAllAsync());
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create(Quiz quiz)
    {
        try
        {
            await _quizRepository.CreateAsync(quiz);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
   
    }

    [HttpPost("get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _quizRepository.GetAllAsync());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("get-user-all-quiz/{userId}")]
    public async Task<IActionResult> GetUserAllQuizzes(string userId)
    {
        try
        {
            return Ok(await _quizRepository.GetQuizByUserIdAsync(userId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

}
