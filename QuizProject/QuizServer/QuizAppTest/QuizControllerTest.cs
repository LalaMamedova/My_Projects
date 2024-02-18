using Castle.Core.Configuration;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using QuizAPI.Controllers;
using QuizApp.Repository.Generic;
using QuizApp.Repository.ModelRepository;
using QuizLib.Data;
using QuizLib.Model;
using System.Runtime.CompilerServices;

namespace QuizAppTest;
[TestFixture]
public class QuizControllerTest
{
    private QuizController quizController;
    public QuizControllerTest()
    {
        QuizRepository quizRepository = A.Fake<QuizRepository>();
        quizController = new(quizRepository);
    }

    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public async Task Create_AddNewTest_ReturnVoid()
    {
        // Arrange
        var newQuiz = new Quiz()
        {
            Title = "Test",
            Type = QuizType.Learning,
            QuizDescription = "Test description",
            Tags = new List<string>() { "Test" },
            UserId = "me",
        };

        // Act
        await quizController.Create(newQuiz);
       
        // Assert
        Assert.Pass("New quiz should be added successfully");
    }


    [Test]
    public async Task GetAll_GetAllQuiz_ReturnAllQuizFromDb()
    {
        // Arrange

        var actionResult = await quizController.GetAll();
        var okResult = actionResult as OkObjectResult;
        var allQuiz = okResult.Value as ICollection<Quiz>;

        //Assert
        Assert.IsNotNull(allQuiz, "allQuiz should't be null");
    }
}