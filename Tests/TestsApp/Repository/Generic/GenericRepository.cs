using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;
using System.Linq.Expressions;
using TestsApp.Repository.İnterfaces;
using TestsLib.DbContexts;
using TestsLib.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestsApp.Repository.Generic;

public class GenericRepository<T,T2> : IGenericRepository<T, T2> where T : class
{
    //private readonly IDbContext _dbContext;
    //private readonly IMongoCollection<T> _collection;

    //public GenericRepository(IDbContext dbContext, string collectionName)
    //{
    //    _dbContext = dbContext;
    //    _collection = _dbContext.MongoDatabase.GetCollection<T>(collectionName);
    //}

    //public async Task CreateAsync(T document)
    //{
    //    await _collection.InsertOneAsync(document:document);
    //}

    //public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
    //{
    //    return await _collection.Find(filter).FirstOrDefaultAsync();
    //}

    //public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>>? filter = null)
    //{
    //    if(filter != null)
    //    {
    //        return await _collection.Find(filter).ToListAsync();
    //    }
    //    return await _collection.Find(new BsonDocument()).ToListAsync();

    //}

    //public async Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T replacement)
    //{
    //    return await _collection.ReplaceOneAsync(filter, replacement);
    //}

    //public async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter)
    //{
    //    return await _collection.DeleteOneAsync(filter);
    //}

    private DbContext _dbContext;
    private DbSet<T> _dbSet;
    private IMapper _mapper;
    public GenericRepository(TestDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbSet = _dbContext.Set<T>();
        _mapper = mapper;
    }

    public async Task CreateAsync(T2 document)
    {
        T original = _mapper.Map<T>(document);

        await _dbContext.AddAsync(original);
    }

    public async Task<T> FindOneAsync(Expression<Func<T, bool>>? filter = null, 
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
            query = query.Where(filter);

        if (include != null)
        {
            query = include(query);
        }

        return await query.FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, 
        IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
            query = include(query);

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync();
    }
    public void ReplaceOne( T2 replacement)
    {
        T original = _mapper.Map<T>(replacement);
        _dbSet.Update(original);
    }

    public void DeleteOne(T entity)
    {
       _dbSet.Remove(entity);
    }
    public async Task SaveAsync()
    {
       await _dbContext.SaveChangesAsync();
    }

    public string GenerateNewId()
    {
        return Guid.NewGuid().ToString();
    }
}
