using EcommerceLib.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeviceApp.Services;

public class UpdateIncludeClassesService
{
    private DbContext _dbContext;
    public UpdateIncludeClassesService(DeviceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void UpdateIncludedProperties(object existingEntity, object newEntity)
    {
        var entityType = existingEntity.GetType();
        var dbContextType = _dbContext.GetType();

        foreach (var propertyInfo in entityType.GetProperties())
        {
            //Check primitive type and null value
            if (!propertyInfo.PropertyType.IsPrimitive &&
                 propertyInfo.PropertyType != typeof(string) &&
                 propertyInfo.GetValue(existingEntity) != null)
            {
                var propertyType = propertyInfo.PropertyType;// is property not null and primitive

                if (propertyInfo.PropertyType.IsGenericType &&
                    propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))//is property collection
                {
                    propertyType = propertyInfo.PropertyType.GetGenericArguments()[0];//if yes, prepertyType is icollection type              
                }

                var dbSetType = typeof(DbSet<>).MakeGenericType(propertyType);//inizialize dbset

                var dbSetProperty = dbContextType.GetProperties()//check dbsets 
                    .FirstOrDefault(prop => prop.PropertyType == dbSetType);

                if (dbSetProperty != null)//if i have dbSet this propertyType
                {
                    var dbSet = (IEnumerable<object>)dbSetProperty.GetValue(_dbContext);//property from dbSet

                    var existingEntityValue = propertyInfo.GetValue(existingEntity);//value from existingEntity
                    var newEntityValue = propertyInfo.GetValue(newEntity);//value from newEntity

                    object? entityInDb = null;
                    //dbSet = dbSet.AsNoTracking();
                    Type collectionType = existingEntityValue.GetType();

                    if (collectionType.IsGenericType &&
                       (collectionType.GetGenericTypeDefinition() == typeof(ICollection<>) ||
                        collectionType.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                        collectionType.GetGenericTypeDefinition() == typeof(HashSet<>))
                    {

                        List<object> newEntityCollection = new List<object>((IEnumerable<object>)newEntityValue);
                        List<object> entityInDbCollection = new List<object>((IEnumerable<object>)existingEntityValue);

                  
                        if (entityInDbCollection.Count > newEntityCollection.Count)
                        {
                            int removeRange = entityInDbCollection.Count - newEntityCollection.Count;

                            for (int i = entityInDbCollection.Count - removeRange; i < entityInDbCollection.Count; i++)
                            {
                                _dbContext.Remove(entityInDbCollection[i]);
                            }
                            _dbContext.SaveChanges();

                        }

                        ForEachEntityCollection(newEntityCollection, entityInDb, dbSet);
                    }

                    else
                    {          
                        entityInDb = EntityInDb(newEntityValue, dbSet);

                        if (entityInDb != null && existingEntityValue != entityInDb)
                        {
                            _dbContext.Update(existingEntityValue);
                        }
                    }
                }
            }
        }
    }
    private object? EntityInDb(object newEntityValue, IEnumerable<object> dbSet)
    {
        PropertyInfo? idProperty = newEntityValue.GetType().GetProperty("Id");
        var idValue = idProperty.GetValue(newEntityValue);

        var entityInDb = dbSet.AsEnumerable()
       .FirstOrDefault(item => idProperty.GetValue(item)
       .Equals(idValue));

        return entityInDb;
    }
    private void ForEachEntityCollection(ICollection<object> collection,  object entityInDb, IEnumerable<object> dbSet )
    {
        PropertyInfo? idProperty = collection.FirstOrDefault().GetType().GetProperty("Id");

        foreach (var collectionItem in collection)
        {
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(collectionItem);

                entityInDb = dbSet.Where(item => idProperty.GetValue(item).Equals(idValue)).FirstOrDefault();

                if (entityInDb == null && collectionItem != null)
                {
                    _dbContext.Add(collectionItem);
                }
                else if (entityInDb != null && entityInDb != collectionItem)
                {
                    _dbContext.Update(collectionItem);
                }
                else if (entityInDb != null && collectionItem == null)
                {
                    _dbContext.Remove(collectionItem);
                }
            }
        }
        _dbContext.SaveChanges();
    }
}
