using System.ComponentModel.DataAnnotations;

namespace EcommerceLib.Models.UserModel.Users;

public class RegistrationRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
