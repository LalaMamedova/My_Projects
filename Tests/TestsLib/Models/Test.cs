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

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("titleImg")]
    public string? TitleImg { get; set; } 

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("questions")]
    public List<TestQuestion> Questions { get; set; }

    [BsonElement("passedCount")]
    public int PassedCount { get; set; } 

    [BsonElement("tags")]
    public List<string> Tags { get; set; }
   
    public string UserId { get; set; }
    public User Autor { get; set; }
}
