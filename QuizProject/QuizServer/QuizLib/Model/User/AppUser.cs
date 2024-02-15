using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;
using QuizLib.Configurator;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
namespace QuizLib.Model.User;
using MongoDB.Bson.Serialization;

[CollectionName("users")]
public class AppUser
{
    [JsonIgnore]
    [BsonIgnore]
    private AppUserConfigurator Configurator;

    [BsonId]
    public Guid Id { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }   

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("userName")]
    public string UserName { get; set; }

    [BsonElement("twoFactorIdendity")]
    public bool TwoFactorIdendity { get; set; }

    [BsonElement("confirmEmail")]
    public bool ConfirmEmail { get; set; }

    [BsonElement("roles")]
    public ICollection<string> Roles { get; set; }

    [BsonElement("userQuizes")]
    public List<Quiz> UserQuizes { get; set; }

    [BsonElement("token")]
    public Token Token { get; set; }

   
}
