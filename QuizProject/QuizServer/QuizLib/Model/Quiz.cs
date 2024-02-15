using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using QuizApp.Model;
using QuizLib.Data.Enums;
using System.Text.Json.Serialization;

namespace QuizLib.Model;
[Serializable]
public class Quiz
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("title")]    
    public string Title { get; set; }

    [BsonElement("titleImg")]
    public string? TitleImg { get; set; }

    [BsonElement("quizDescription")]
    public string? QuizDescription { get; set; }
    
    [BsonElement("tags")]
    public List<string> Tags { get; set; }


    [BsonElement("quizQuestions")]
    public ICollection<QuizQuestion>? QuizQuestions { get; set; }

    [BsonElement("quizResults")]
    public ICollection<QuizResult> QuizResults { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public QuizType Type { get; set; }

    [JsonIgnore]
    [BsonIgnore]
    public QuizResult? UserReslut { get; set; }

    [BsonElement("updateTime")]
    public DateTime UpdateTime { get;set; }

}
