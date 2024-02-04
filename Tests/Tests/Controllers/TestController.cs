using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestsApp.Services.HttpRequests;
using TestsLib.Dto;
using TestsLib.Models;

namespace Tests.Controllers
{
    public class TestController : Controller
    {
        private TestRequests _testRequests;
        public TestController(TestRequests testRequests)
        {
            _testRequests = testRequests;
        }

        [HttpGet("/Test/Catalog/{tag}")]
        public async Task<IActionResult> Catalog(string tag)
        {
            IEnumerable<Test> response = await _testRequests.GetTestByTag(tag);
            return View(response);
        }

        [HttpGet("/Test/{id}")]
        public async Task<IActionResult> Detail(string id)
        {
            Test response = await _testRequests.GetTest(id);
            return View(response);
        }


        [HttpPost("/Test/NextQuestions/{index:int}")]
        public IActionResult NextQuestions(int index, [FromBody] Test currentTest)
        {
            if (index < currentTest.Questions.Count())
            {
                TestQuestion Questions = currentTest.Questions.ElementAt(index);
                return Json(Questions);
            }
            return Json("End");
        }


        [HttpGet("/Test/CreateTest")]
        public IActionResult CreateTest()
        {
            string userCookieValue = HttpContext.Request.Cookies["User"];

            if (userCookieValue.Length > 0)
            {
                TestDto testModel = new TestDto
                {
                    Questions = new List<TestQuestionDto>
                    {
                        new TestQuestionDto()
                        {
                            Choice = new List<string>(["", "", "", ""]),
                        }
                    },
                    Tags = new List<string>([""])  
                };
                return View(testModel);
            }
            else
            {
                return Redirect("/User/Signin");
            }
        }

        [HttpPost("/Test/CreateNewTestPost")]
        public async Task<IActionResult> CreateNewTestPost(TestDto newTest, string action)
        {
            if (action == "Submit")
            {
                await PostTest(newTest);
                return Redirect("/Home/Index");
            }
            else if (action == "New Tag")
            {
                await CreateNewTag(newTest);
            }
            else
            {
                await CreateNewQuestion(newTest);
            }


            return View("CreateTest", newTest);
        }


        [HttpPost("/Test/CreateNewQuestion")]
        public async Task<IActionResult> CreateNewQuestion(TestDto newTest)
        {
            if (newTest.Questions != null)
            {
                newTest.Questions.Add(new TestQuestionDto() { Choice = new(["", "", "", ""]) });
            }
            else
            {
                newTest.Questions = new List<TestQuestionDto>()
                {
                    new TestQuestionDto()
                    {
                        Choice = new(),
                    }
                };
            }

            return View("CreateTest", newTest);
        }


        [HttpPost("/Test/CreateNewTag")]
        public async Task<IActionResult> CreateNewTag(TestDto newTest)
        {
            newTest.Tags.Add("");
            return View("CreateTest", newTest);
        }


        [HttpPost("/Test/PostTest")]
        public async Task<IActionResult> PostTest(TestDto newTest)
        {
            Console.WriteLine(newTest);

            string? userId = HttpContext.Session.GetString("User");
            newTest.UserId = userId;

            await _testRequests.PostTest(newTest);
            return View("CreateTest", newTest);
        }
    }
}
