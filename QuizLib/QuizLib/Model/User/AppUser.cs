using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Driver;
using MongoDbGenericRepository.Attributes;

namespace QuizLib.Model.User;
[CollectionName("users")]
public class AppUser : MongoIdentityUser<Guid>
{
    public List<Quiz> UserQuizes { get; set; }
}
