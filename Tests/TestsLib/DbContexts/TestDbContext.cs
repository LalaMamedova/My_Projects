using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestsLib.Models;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;

namespace TestsLib.DbContexts;

public class TestDbContext
{
    private MongoClient client;

    public  IMongoDatabase mongoDatabase;
    public  IMongoCollection<Test> Tests;
    public  IMongoCollection<TestQuestion> TestQuestions;

    public TestDbContext()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = MongoClientSettings.FromConnectionString(config["MongoDb:Connection"]);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        client = new MongoClient(settings);
        mongoDatabase = client.GetDatabase(config["MongoDb:DataBaseName"]);
      
        Tests = mongoDatabase.GetCollection<Test>(config["MongoDb:TestCollectionName"]);
    }
}
