using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TestsApp.Repository.Generic;
using TestsApp.Repository.İnterfaces;
using TestsLib.DbContexts;
using TestsLib.Dto;
using TestsLib.Models;
using TestsLib.Models.UserModels;

namespace TestsApp.Repository.ModelReposotry;

public class TestRepository
{
    private IGenericRepository<Test,TestDto> _repository;
    private IGenericRepository<User,UserDto> _userRepository;
    private IGenericRepository<TestQuestion, TestQuestionDto> _questionRep;

    public TestRepository(IGenericRepository<Test, TestDto> repository,
                          IGenericRepository<User, UserDto> userRepository,
                          IGenericRepository<TestQuestion, TestQuestionDto> questionRep)
    {

        _repository = repository;
        _userRepository = userRepository;
        _questionRep = questionRep;

        //var config = new ConfigurationBuilder()
        //.SetBasePath(Directory.GetCurrentDirectory())
        //.AddJsonFile("appsettings.json")
        //.Build();
        //_repository = new(_context, config["MongoDb:TestCollectionName"]);


    }

    public async Task CreateAsync(TestDto test)
    {
        test.Id = _repository.GenerateNewId();

        foreach (var question in test.Question)
        {
            question.Id = _repository.GenerateNewId();
            question.TestId = test.Id;
        }

        await _repository.CreateAsync(test);
        await _repository.SaveAsync();

        //User user = await _userRepository.GetUserById(userId);
        //UserAndTests userAndTests = new()
        //{
        //    User = user,
        //    Tests = new[] { test },
        //};

        //await _userTestsRepository.CreateAsync(userAndTests);
       
    }
    public async Task DeleteAsync(string userId, string id)
    {
        var delete = await _repository.FindOneAsync(x => x.Id == id);
        _repository.DeleteOne(delete);
       await _repository.SaveAsync();
    }
    public async Task<IEnumerable<Test>> GetAll()
    {
        return await _repository.FindManyAsync(include:
                      x=>x.Include(x=>x.Autor));
    }

    public async Task<IEnumerable<string>> GetMostPopularTags()
    {
        var response = await _repository.FindManyAsync();

        var allTags = response.SelectMany(x => x.Tags).ToList();

        var tagCounts = allTags.GroupBy(tag => tag)
                              .Select(group => new { Tag = group.Key, Count = group.Count() })
                              .OrderByDescending(x => x.Count);

       return tagCounts.Take(5).Select(x => x.Tag).ToList();

    }
    public async Task<IEnumerable<Test>> GetAllTestsByTag(string tag)
    {
        return await _repository
            .FindManyAsync(filter:x => x.Tags.Contains(tag),
            include:x=>x.Include(x=>x.Questions));
    }

    public async Task<Test> GetById(string id)
    {
        return await _repository.FindOneAsync(x=>x.Id == id,x=>x.Include(x=>x.Questions));
    }

}
