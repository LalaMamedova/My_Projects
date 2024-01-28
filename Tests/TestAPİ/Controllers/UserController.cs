using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestsApp.Repository.ModelReposotry;
using TestsLib.Dto;
using TestsLib.Models.UserModels;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> SingUp([FromBody] UserDto user)
        {
            await _userRepository.CreateAsync(user);
            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SingIn([FromBody] RequestUser userDto)
        {
            try
            {
                return Ok(await _userRepository.GetUser(userDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
    
        }

    }
}
