using AutoMapper;
using DeviceApp.Repo.Interface;
using DeviceApp.Services;
using EcommerceLib.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace DeviceApp.Repo.General;

public class Repository<T, T2> : IGenericRepository<T, T2> where T : class
{
    private readonly DeviceDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    private readonly IMapper _mapper;
    public Repository(DeviceDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbSet = _dbContext.Set<T>();

        _mapper = mapper;
    }

    public async Task AddAsync(T2 dto, Expression<Func<T, bool>>? filter = null)
    {
        if (filter != null)
        {
            var exist = await _dbSet.AnyAsync(filter);
            if (exist)
            {
                throw new ArgumentException("This item alredy exist");
            }
        }

        var entity = _mapper.Map<T>(dto);
        await _dbSet.AddAsync(entity);
    }


    public async Task<T> FindByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }


    public async Task<IEnumerable<T>> GetAllAsync
        (Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        Action<T>? additionalFilter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (include != null)
            query = include(query);

        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);


        if (additionalFilter != null)
        {
            await query.ForEachAsync(additionalFilter);
        }


        return await query.ToListAsync();
    }

    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter,
        bool tracked = true, Func<IQueryable<T>,
        IIncludableQueryable<T, object>>? include = null)
    {
        IQueryable<T> query;

        if (tracked)
            query = _dbSet;
        else
            query = _dbSet.AsNoTracking();

        if (filter != null)
            query = query.Where(filter);

        if (include != null)
        {
            query = include(query);
        }
        if (query != null)
        {
            return await query.FirstOrDefaultAsync();
        }
        throw new NullReferenceException("Not found");
    }

    public async Task RemoveAsync(int id, Func<IQueryable<T>, 
                                  IIncludableQueryable<T, object>>? include = null,
                                  Expression<Func<T, bool>>? filter = null)
    {
        T? entity = await FindByIdAsync(id);

        if (include != null && filter!=null)
        {
            IQueryable<T> query = _dbSet;
            query = include(query);
            entity = query.FirstOrDefault(filter);
        }
        if (entity != null)
        {
            _dbContext.Remove(entity);
        }
        else
        {
            throw new ArgumentNullException("Not found");
        }
    }

    public void RemoveRange(IEnumerable<T2> dto)
    {
        var entity = _mapper.Map<T>(dto);
        _dbSet.RemoveRange(entity);
    }

    public async Task<T> Update(T2 dto,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
    Expression<Func<T, bool>>? filter = null)
    {
        var entity = _mapper.Map<T>(dto);

        if (include != null)
        {
            IQueryable<T> query = _dbSet;
            query = include(query);

            var existingEntity = filter != null ? await query.FirstOrDefaultAsync(filter) : null;

            if (existingEntity != null)
            {
                UpdateIncludeClassesService updateClasses = new(_dbContext);
                updateClasses.UpdateIncludedProperties(existingEntity, entity);
            }
            _dbContext.Update(entity);

        }
        else
        {
            _dbContext.Update(entity);
        }

        await _dbContext.SaveChangesAsync();
        return entity;

    }


    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }


}
