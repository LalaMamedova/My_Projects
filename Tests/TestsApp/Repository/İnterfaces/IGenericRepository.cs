using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace TestsApp.Repository.İnterfaces;

public interface IGenericRepository<T,T2>
{
    public  Task CreateAsync(T2 entity);

    public Task<T> FindOneAsync(Expression<Func<T, bool>>? filter,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    public Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>>? filter = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    public void ReplaceOne(T2 replacement);
    public  string GenerateNewId();
    public void DeleteOne(T entity);
    public Task SaveAsync();
}
