using QuizLib.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Repository.Generic;

public interface IGenericRepository<T> where T : class
{
    public Task CreateAsync(T entity);
    public Task<T> FindOneAsync(Expression<Func<T, bool>>? filter);
    public Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>>? filter = null);
    public Task ReplaceOne(Expression<Func<T, bool>> filter, T replacement);
    public Task DeleteOne(Expression<Func<T, bool>> filter);
    public void ChoiceDbAndCollection(IMongoDatabaseContext dbContext, string collectionNameFromUserSecret);
}
