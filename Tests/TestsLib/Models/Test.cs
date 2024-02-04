using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using TestsLib.Models.UserModels;
using System.Text.Json.Serialization;


namespace TestsLib.Models;
[Serializable]
public class Test
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string? TitleImg { get; set; } 

    public string Description { get; set; }

    public List<TestQuestion> Questions { get; set; }

    public int PassedCount { get; set; } 

    public List<string> Tags { get; set; }
   
    public string UserId { get; set; }
    public User Autor { get; set; }
}
