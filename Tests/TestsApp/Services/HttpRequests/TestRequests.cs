using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;
using TestsApp.Services.Interfaces;
using TestsLib.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using TestsLib.Models.UserModels;

namespace TestsApp.Services.HttpRequests;

public class TestRequests:IHttpRequest
{
    private HttpClient _httpClient;
    
    public TestRequests(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<Test>?> TestGetAll()
    {
        var response = await _httpClient.GetAsync("https://localhost:7787/api/Test");
        string textResult = await response.Content.ReadAsStringAsync();
        IEnumerable<Test> result =  JsonConvert.DeserializeObject<List<Test>>(textResult);
        return result != null ? result : null; 
        
    }

    public async Task<IEnumerable<Test>?> GetTestByTag(string tag)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7787/api/Test/GetTestsByTag/{tag}");
        string textResult = await response.Content.ReadAsStringAsync();
        IEnumerable<Test> result = JsonConvert.DeserializeObject<IEnumerable<Test>>(textResult);

        return result != null ? result : null;

    }

    public async Task<IEnumerable<string>?> GetPopularTags()
    {
        var response = await _httpClient.GetAsync("https://localhost:7787/api/Test/GetPopularTags");
        string textResult = await response.Content.ReadAsStringAsync();
        IEnumerable<string> result = JsonConvert.DeserializeObject<List<string>>(textResult);
        return result != null ? result : null;

    }
    public async Task<Test?> GetTest(string id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7787/api/Test/GetById/{id}");
        string textResult = await response.Content.ReadAsStringAsync();
        Test result = JsonConvert.DeserializeObject<Test>(textResult);
        return result != null ? result : null;

    }
  
    public async Task PostTest(Test newTest)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newTest), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("https://localhost:7787/api/Test", jsonContent);

    }
}
