using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsApp.Services.Interfaces;
using TestsLib.Dto;
using TestsLib.Models;
using TestsLib.Models.UserModels;

namespace TestsApp.Services.HttpRequests;

public class UserRequest:IHttpRequest
{
    private HttpClient _httpClient;

    public UserRequest(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task SingUpRequest(UserDto requestUser)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestUser), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7787/api/User/SignUp", jsonContent);
       
    }
    public async Task<User?> SinginRequest(RequestUser requestUser)
    {
        var jsonContent = new StringContent(JsonConvert.SerializeObject(requestUser), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("https://localhost:7787/api/User/SignIn", jsonContent) ;
        string textResult = await response.Content.ReadAsStringAsync();

        User result = JsonConvert.DeserializeObject< User > (textResult);
        return result != null ? result : null;
    }
}
