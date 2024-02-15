using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using QuizApp.Repository.Generic;
using QuizLib.DatabaseContext;
using QuizLib.Model;
using QuizLib.Model.User;
using System.Text;

namespace QuizApp.Repository.ModelRepository;

public class QuizRepository
{
    private  IGenericRepository<Quiz> _genericRepository;
    private  IMongoDatabaseContext _quizContext;
    public QuizRepository(IGenericRepository<Quiz> genericRepository,
                          IMongoDatabaseContext quizContext)
    {
        _genericRepository = genericRepository;
        _quizContext = quizContext;

        _genericRepository.ChoiceDbAndCollection(_quizContext, "Mongo:QuizCollectionName");
    }

    public async Task CreateAsync(Quiz quiz)
    {
        //var user = _userManager.Users
        //    .Where(x=>x.Id.ToString() == quiz.UserId)
        //    .FirstOrDefault();
        //if(user == null) {
        //    throw new NullReferenceException("User not found");
        //}

        //if (user.UserQuizes == null)
        //    user.UserQuizes = new();
        
        //user.UserQuizes.Add(quiz);
        //var a = await _userManager.UpdateAsync(user);

        await _genericRepository.CreateAsync(quiz);
       
    }
    public async Task Delete(ObjectId id)
    {
         await _genericRepository.DeleteOne(x=>x.Id == id);
    }
    public async Task<IEnumerable<Quiz>> GetAllAsync()
    {
        return await _genericRepository.FindManyAsync(); 
    }
    public async Task<IEnumerable<string>> GetPopularyTag()
    {
        var response = await _genericRepository.FindManyAsync();

        var allTags = response.SelectMany(x => x.Tags).ToList();

        var tagCounts = allTags.GroupBy(tag => tag)
                              .Select(group => new { Tag = group.Key, Count = group.Count() })
                              .OrderByDescending(x => x.Count);

        return tagCounts.Take(5).Select(x => x.Tag).ToList();
    }

    public async Task<IEnumerable<Quiz>> GetQuizByTagAsync(string tag)
    {
        return await _genericRepository
           .FindManyAsync(filter: x => x.Tags.Contains(tag));

    }

    public async Task<Quiz> GetQuiz(ObjectId id)
    {
        return await _genericRepository
           .FindOneAsync(x=>x.Id == id);
    }

    public async Task<IEnumerable<Quiz>> GetQuizByUserIdAsync(string id)
    {
        return await _genericRepository
           .FindManyAsync(x => x.UserId == id);
    }

    public async Task UpdateAsync(Quiz quiz)
    {
        quiz.UpdateTime = DateTime.Now;
        await _genericRepository
           .ReplaceOne(x=>x.Id==quiz.Id, quiz);
    }
}
