using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace TestsLib.Models.UserModels;

public class RequestUser
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

}
