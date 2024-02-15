using MongoDB.Bson;
using QuizLib.Model;
using QuizLib.Model.User;

namespace QuizLib.Dto.Response;

public class LoginResponse
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }   
    public Token UserToken { get; set; }    
    public List<Quiz> UserQuizes { get; set; }  
}
