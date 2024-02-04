using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using QuizLib.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Repository.Generic;

public class GenericMongoRepository<T> : IGenericRepository<T> where T : class
{
    private IMongoDbContext mongoDbContext;
    private IMongoCollection<T> _collection;

    public void ChoiceDbAndCollection(IMongoDbContext dbContext, string collectionNameFromUserSecret)
    {
        var config = new ConfigurationBuilder()
           .AddUserSecrets<IMongoDbContext>()
           .Build();
        
        mongoDbContext = dbContext;
        _collection = mongoDbContext.MongoDatabase.GetCollection<T>(config[collectionNameFromUserSecret]);
    }

    public async Task CreateAsync(T entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task  DeleteOne(Expression<Func<T, bool>> filter)
    {
       await _collection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>>? filter = null)
    {
        if (filter != null)
        {
            return await _collection.Find(filter).ToListAsync();
        }
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> FindOneAsync(Expression<Func<T, bool>>? filter)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    public async Task ReplaceOne(Expression<Func<T, bool>> filter, T replacement)
    {
         await _collection.ReplaceOneAsync(filter, replacement);
    }


}
