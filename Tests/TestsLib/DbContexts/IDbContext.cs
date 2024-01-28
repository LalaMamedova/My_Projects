
using MongoDB.Driver;

namespace TestsLib.DbContexts;

public interface IDbContext
{
    public IMongoDatabase MongoDatabase { get; set; }

}
