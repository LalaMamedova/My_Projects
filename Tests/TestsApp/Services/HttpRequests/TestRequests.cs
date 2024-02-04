using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;
using TestsApp.Services.Interfaces;
using TestsLib.Models;
using MongoDB.Bson.Serialization.Attributes;
using System.Text;
using TestsLib.Dto;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using TestsLib.Models.UserModels;

namespace TestsApp.Services.HttpRequests;

public class TestRequests:IHttpRequest
{
    private HttpClient _httpClient;
    
    public TestRequests(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    private async Task<T?> RequestTemplate<T>(string uri) where T: class { 

        var response = await _httpClient.GetAsync(uri);
        string textResult = await response.Content.ReadAsStringAsync();

        T result = JsonConvert.DeserializeObject<T>(textResult);
        return result != null ? result : null;
    }
    public async Task<IEnumerable<Test>?> TestGetAll()
    {
        return await RequestTemplate<List<Test>>("https://localhost:7787/api/TestApi");       
    }

    public async Task<IEnumerable<Test>?> GetTestByTag(string tag)
    {
        var result = await RequestTemplate<List<Test>>($"https://localhost:7787/api/TestApi/GetTestsByTag/{tag}");
        return result;
    }

    public async Task<IEnumerable<string>?> GetPopularTags()
    {
        var result = await RequestTemplate<List<string>>($"https://localhost:7787/api/TestApi/GetPopularTags");
        return result;
    }
    public async Task<Test?> GetTest(string id)
    {
        var result =  await RequestTemplate<Test>($"https://localhost:7787/api/TestApi/GetById/{id}");
        return result;
    }

    public async Task PostTest(TestDto newTest)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(newTest), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync("https://localhost:7787/api/TestApi/CreateTest", jsonContent);

        //var json = JsonConvert.SerializeObject(newTest);
        //try
        //{
        //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7787/api/TestApi");
        //    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await _httpClient.SendAsync(request);
        //    Console.WriteLine(response);
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex.Message);
        //}

    }
    

}
