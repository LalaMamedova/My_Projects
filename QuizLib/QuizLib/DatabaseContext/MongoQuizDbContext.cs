using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using QuizLib.Model;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace QuizLib.DatabaseContext;

public class MongoQuizDbContext : IMongoDbContext
{
    private readonly MongoClient _client;

    public IMongoDatabase MongoDatabase { get; set; }
    public IMongoCollection<Quiz>  Quizs { get; set; }
    public IMongoCollection<QuizQuestion> QuizQuestions { get; set; }

    public MongoQuizDbContext()
    {
        var config = new ConfigurationBuilder()
           .AddUserSecrets<MongoQuizDbContext>()
           .Build();

        var connectionString = config["Mongo:Connection"];
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);

        MongoDatabase = _client.GetDatabase(config["Mongo:DataBaseName"]);
        Quizs = MongoDatabase.GetCollection<Quiz>(config["Mongo:QuizCollectionName"]);
        QuizQuestions = MongoDatabase.GetCollection<QuizQuestion>(config["Mongo:QuizQuestionCollectionName"]);

    }

}
