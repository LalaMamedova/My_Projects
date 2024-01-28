using Microsoft.AspNetCore.Mvc;
using TestsApp.Services.HttpRequests;
using TestsLib.Models;

namespace Tests.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private TestRequests _testRequests;
        public TestController(ILogger<TestController> logger, TestRequests testRequests)
        {
            _logger = logger;
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


        [HttpPost("/Test/NextQuestion/{index:int}")]
        public IActionResult NextQuestion(int index, [FromBody] Test currentTest)
        {
            if (index < currentTest.Questions.Count())
            {
                TestQuestion question = currentTest.Questions.ElementAt(index);
                return Json(question);
            }
            return Json("End");
        }


        [HttpGet("/Test/CreateTest")]
        public IActionResult CreateTest()
        {
            string userCookieValue = HttpContext.Request.Cookies["User"];

            if (userCookieValue.Length > 0)
            {
                var testModel = new Test
                {
                    Questions = new List<TestQuestion>()
                    {
                        new TestQuestion()
                        {
                            Choice = new List<string>(["", "", "", ""]),
                        }
                    },
                    Tags = new List<string>(["Test","",""])  
                };

                ModelState.Clear();
                return View(testModel);
            }
            else
            {
                return Redirect("/User/Signin");
            }
        }

        [HttpPost("/Test/CreateNewTestPost")]
        public async Task<IActionResult> CreateNewTestPost(Test newTest, string action)
        {
            if (action == "Submit")
            {
                await PostTest(newTest);
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
        public async Task<IActionResult> CreateNewQuestion(Test newTest)
        {
            if (newTest.Questions != null)
            {
                newTest.Questions.Add(new TestQuestion() { Choice = new(["", "", "", ""]) });
            }
            else
            {
                newTest.Questions = new List<TestQuestion>()
                {
                    new TestQuestion()
                    {
                        Choice = new(),
                    }
                };
            }

            return View("CreateTest", newTest);
        }


        [HttpPost("/Test/CreateNewTag")]
        public async Task<IActionResult> CreateNewTag(Test newTest)
        {
            newTest.Tags.Add("");
            return View("CreateTest", newTest);
        }


        [HttpPost("/Test/PostTest")]
        public async Task<IActionResult> PostTest(Test newTest)
        {
            //newTest = JsonConvert.DeserializeObject<Test>(TempData["Test"].ToString());
            await _testRequests.PostTest(newTest);
            return Redirect("/Home/Index");
        }
    }
}
