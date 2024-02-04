using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace QuizLib.Model.User;
[CollectionName("usersRole")]
public class AppUserRole:MongoIdentityRole<Guid>
{
}
