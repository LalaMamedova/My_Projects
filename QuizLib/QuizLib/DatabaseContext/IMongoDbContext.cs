using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.DatabaseContext;

public interface IMongoDbContext
{
    public IMongoDatabase MongoDatabase { get; set; }
}
