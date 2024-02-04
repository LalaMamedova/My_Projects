using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using QuizApp.Repository.Generic;
using QuizLib.DatabaseContext;
using QuizLib.Model;
using QuizLib.Model.User;
using System.Text;

namespace QuizApp.Repository.ModelRepository;

public class QuizRepository
{
    private readonly IGenericRepository<Quiz> _genericRepository;
    private readonly IMongoDbContext _quizContext;
    private readonly UserManager<AppUser> _userManager;
    public QuizRepository(IGenericRepository<Quiz> genericRepository,
                          IMongoDbContext quizContext,
                          UserManager<AppUser> userManager)
    {
        _genericRepository = genericRepository;
        _quizContext = quizContext;
        _userManager = userManager;

        _genericRepository.ChoiceDbAndCollection(_quizContext, "Mongo:QuizCollectionName");
    }

    public async Task CreateAsync(string userId,Quiz quiz)
    {
        await _genericRepository.CreateAsync(quiz);

        var user = _userManager.Users
            .Where(x=>x.Id.ToString() == userId)
            .FirstOrDefault();

        if(user == null) {
            throw new NullReferenceException("User not found");
        }

        quiz.UserId = userId;
        user.UserQuizes.Add(quiz);
        await _userManager.UpdateAsync(user);
    }

    public async Task<IEnumerable<Quiz>> GetAllAsync()
    {
        return await _genericRepository.FindManyAsync(); 
    }

}
