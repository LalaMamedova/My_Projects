using MongoDB.Driver;
using System.Linq.Expressions;
using TestAPI.Repository.İnterfaces;
using TestsLib.DbContexts;

namespace TestAPI.Repository.Generic;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly TestDbContext _testDbContext;
    private readonly IMongoCollection<T> _collection;

    public GenericRepository(TestDbContext testDbContext, string collectionName)
    {
        _testDbContext = testDbContext;
        _collection = _testDbContext.mongoDatabase.GetCollection<T>(collectionName);
    }

    public async Task CreateAsync(T document)
    {
        await _collection.InsertOneAsync(document);
    }

    public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> FindManyAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T replacement)
    {
        return await _collection.ReplaceOneAsync(filter, replacement);
    }

    public async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.DeleteOneAsync(filter);
    }
}
