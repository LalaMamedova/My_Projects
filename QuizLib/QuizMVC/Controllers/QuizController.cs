using Microsoft.AspNetCore.Mvc;
using QuizApp.Model;
using QuizApp.Services.ModelServices;
using QuizLib.Model;
using System;

namespace QuizMVC.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuizService _service;
        public QuizController(ILogger<HomeController> logger,
                            QuizService sevice)
        {
            _logger = logger;
            _service = sevice;
        }

        public IActionResult Index()
        {
            Quiz newQuiz = new()
            {

                Tags = new List<string>([""]),
            };

            return View(newQuiz);
        }

        public IActionResult AddTag(Quiz model)
        {
            _service.AddTag(model);
            ViewData["Info"] = "Added tag";
            return View("Index", model);
        }

        public IActionResult AddQuestion(Quiz model)
        {
            _service.AddQuestion(model);
            return View("Questions", model);
        }

        public IActionResult AddOption(Quiz model)
        {
            int questionIndex = int.Parse(Request.Form["questionIndex"]);
            _service.AddOption(model, questionIndex);
            return View("Questions", model);
        }

        public IActionResult RemoveQuestion(Quiz model)
        {
            int questionIndex = int.Parse(Request.Form["questionIndex"]);

            _service.RemoveQuestion(model, questionIndex);

            return View("Questions", model);
        }
        public IActionResult RemoveOption(Quiz model)
        {
            var a = Request.Form["questionIndex"];
            int questionIndex = int.Parse(Request.Form["questionIndex"]);
            int optionIndex = int.Parse(Request.Form["optionIndex"]);

            _service.RemoveOption(model, questionIndex, optionIndex);

            return View("Questions", model);
        }

        public IActionResult NextToResult(Quiz model)
        {
            model.QuizResult = new()
            {
                ResultDescription = new([""]),
                ResultTitle = new([""])
            };

            model.RightAnswers = new([""]);
            model.RightAnswers.Capacity = model.QuizQuestions.Count;

            return View("Result", model);
        }
        public IActionResult NextToQuestions(Quiz model)
        {
            model.QuizQuestions = new()
            {
                new QuizQuestion
                {
                    Options = new(["",""]),
                },
            };
           
            return View("Questions", model);
        }
        public async Task<IActionResult> AddNewTest(Quiz model, string userId)
        {
            await _service.AddNewTest(model, userId);
            ViewData["Info"] = "Sucsses";
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessResultForm(Quiz model, string method)
        {
            try
            {
                switch (method)
                {
                    case "AddQuestion":
                        return AddQuestion(model);
                   
                }
            }
            catch (Exception ex)
            {
                ViewData["Info"] = ex.Message;
            }

            return View("Questions", model);
        }
        [HttpPost]
        public async Task<IActionResult> ProcessQuestionForm(Quiz model, string method)
        {
            try
            {                
                switch (method)
                {
                    case "AddQuestion":
                        return AddQuestion(model);
                    case "AddOption":
                        return AddOption(model);
                    case "RemoveOption":
                        return RemoveOption(model);
                    case "RemoveQuestion":
                        return RemoveQuestion(model);
                    case "Next":
                        return NextToResult(model);
                }
            }
            catch (Exception ex)
            {
                ViewData["Info"] = ex.Message;
            }

            return View("Questions",model);
        }
        public async Task<IActionResult> ProcessIndexForm(Quiz model, string method)
        {
            try
            {
                switch (method)
                {
                    case "AddTag":
                        return AddTag(model);
                    case "Next":
                        return NextToQuestions(model);
                }
            }
            catch (Exception ex)
            {
                ViewData["Info"] = ex.Message;
            }

            return View("Index");
        }
    }
}
