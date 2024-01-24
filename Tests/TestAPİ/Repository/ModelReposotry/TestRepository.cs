using MongoDB.Driver;
using TestAPI.Repository.Generic;
using TestsLib.DbContexts;
using TestsLib.Models;

namespace TestAPI.Repository.ModelReposotry;

public class TestRepository
{
    private GenericRepository<Test> _repository;
    private TestDbContext _context;

    public TestRepository(TestDbContext context)
    {
        _context = context;

        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        _repository = new(_context, config["MongoDb:TestCollectionName"]);
    }

    public async Task CreateAsync(Test test)
    {
        await _repository.CreateAsync(test);
        
    } 

}
