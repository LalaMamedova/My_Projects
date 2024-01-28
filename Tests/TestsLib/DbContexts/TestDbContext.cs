using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestsLib.Models;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using TestsLib.Models.UserModels;

namespace TestsLib.DbContexts;

public class TestDbContext:DbContext
{
    //private MongoClient client;
    //public IMongoDatabase MongoDatabase { get; set; }
    //public IMongoCollection<Test> Tests;

    //public TestDbContext()
    //{
    //    //var config = new ConfigurationBuilder()
    //    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    //    .AddJsonFile("appsettings.json")
    //    //    .Build();

    //    //var settings = MongoClientSettings.FromConnectionString(config["MongoDb:Connection"]);
    //    //settings.ServerApi = new ServerApi(ServerApiVersion.V1);

    //    //client = new MongoClient(settings);
    //    //MongoDatabase = client.GetDatabase(config["MongoDb:DataBaseName"]);

    //    //Tests = MongoDatabase.GetCollection<Test>(config["MongoDb:TestCollectionName"]);
    //}
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestQuestion> TestQuestions { get; set; }
    public DbSet<User> Users { get; set; }  
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    public TestDbContext() { }  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

        var connectionString = config["SQL:Connection"];
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var tests = modelBuilder.Entity<Test>();
        var testquestions = modelBuilder.Entity<TestQuestion>();
        var user = modelBuilder.Entity<User>();

        user.Property(x => x.Id)
        .ValueGeneratedOnAdd()
        .HasDefaultValueSql("NEWID()");

        user.HasMany(x => x.Tests)
            .WithOne(x => x.Autor)
            .HasForeignKey(x => x.UserId);

      
        //-----------------------------------------
        tests.HasIndex(x => x.Id);

        tests.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");

        tests.HasMany(x => x.Questions)
            .WithOne(x => x.Test)
            .HasForeignKey(x => x.TestId);

        //-----------------------------------------

        testquestions.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()");
    }

}
