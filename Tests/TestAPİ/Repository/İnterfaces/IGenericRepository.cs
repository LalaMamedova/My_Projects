using MongoDB.Driver;
using System.Linq.Expressions;

namespace TestAPI.Repository.İnterfaces;

public interface IGenericRepository<T>
{
    public Task CreateAsync(T document);
    public  Task<T> FindOneAsync(Expression<Func<T, bool>> filter);
    public  Task<List<T>> FindManyAsync(Expression<Func<T, bool>> filter);
    public  Task<ReplaceOneResult> ReplaceOneAsync(Expression<Func<T, bool>> filter, T replacement);
    public  Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter);

}
