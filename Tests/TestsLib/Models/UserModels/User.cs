using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestsLib.Models.UserModels;
[BsonIgnoreExtraElements]

public class User
{
    public string Id { get; set; }


    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }
    public bool isAuth { get; set; }

    public ICollection<Test> Tests { get; set; }
}
