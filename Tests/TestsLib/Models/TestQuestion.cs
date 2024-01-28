using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
namespace TestsLib.Models;

[Serializable]
public class TestQuestion
{
    //[BsonId]
    public string Id { get; set; }

    [BsonElement("answer")]
    public string? Answer { get; set; }

    [BsonElement("questionDescription")]
    public string QuestionDescription { get; set; }

    [BsonElement("choice")]
    public List<string> Choice { get; set; }

    public Test Test { get; set; }
    public string TestId {  get; set; }


}
