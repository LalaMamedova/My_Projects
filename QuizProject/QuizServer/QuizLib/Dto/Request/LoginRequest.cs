using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuizLib.Dto.Request;

public class LoginRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
