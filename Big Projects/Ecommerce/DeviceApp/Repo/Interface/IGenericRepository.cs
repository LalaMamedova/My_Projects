using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DeviceApp.Repo.Interface;

public interface IGenericRepository<T, T2> where T : class
{
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, 
        bool tracked = true, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Action<T>? additionalFilter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

    Task<T> FindByIdAsync(int id);
    Task<T> Update(T2 dto, 
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
    Expression<Func<T, bool>>? filter = null);
    Task AddAsync(T2 dto, Expression<Func<T, bool>>? filter = null);
    Task RemoveAsync(int id,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Expression<Func<T, bool>>? filter = null);
    Task SaveChangesAsync();
    void RemoveRange(IEnumerable<T2> dto);

}
