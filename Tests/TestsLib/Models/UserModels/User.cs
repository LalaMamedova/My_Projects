using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestsLib.Models.UserModels;
[BsonIgnoreExtraElements]

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool isAuth { get; set; }

    public List<Test> Tests { get; set; }
}
