using Microsoft.Extensions.Logging;
using QuizApp.Repository.ModelRepository;
using QuizLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Services.ModelServices;

public class QuizService
{
    private readonly QuizRepository _repository;
    public QuizService(QuizRepository repository)
    {
        _repository = repository;
    }
    public async Task AddNewTest(Quiz quiz, string userId)
    {
        try
        {
            await _repository.CreateAsync(userId, quiz);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void AddTag(Quiz quiz)
    {
        if (quiz.Tags != null)
            quiz.Tags.Add("");
        else 
            quiz.Tags = new List<string>(); 
    }
    public void AddOption(Quiz quiz,int index)
    {
        if (quiz.QuizQuestions[index].Options != null)
            quiz.QuizQuestions[index].Options.Add("");
        else
            quiz.QuizQuestions[index].Options = new List<string>([""]);
        
    }

    public void AddQuestion(Quiz quiz)
    {
        quiz.QuizQuestions = quiz.QuizQuestions != null
        ? quiz.QuizQuestions.Append(new QuizQuestion() { Options = new List<string> { "", "" } }).ToList()
        : new List<QuizQuestion> { new QuizQuestion() { Options = new List<string> { "", "" } } };
    }

    public void RemoveOption(Quiz quiz, int questionIndex, int index)
    {
        quiz.QuizQuestions[questionIndex].Options.RemoveAt(index);
    }
    public void RemoveQuestion(Quiz quiz, int questionIndex)
    {
        quiz.QuizQuestions.RemoveAt(questionIndex);
    }
}
