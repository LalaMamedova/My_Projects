using System.ComponentModel.DataAnnotations;

namespace QuizLib.Dto.Request;

public class ReqisterRequest
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Username { get; set; }
}
