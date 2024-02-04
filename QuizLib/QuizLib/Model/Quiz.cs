using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Converters;
using QuizApp.Model;
using System.Text.Json.Serialization;

namespace QuizLib.Model;
public enum QuizType { ForFun, Learning }
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
    public List<QuizQuestion> QuizQuestions { get; set; }

    [BsonElement("quizResult")]
    public QuizResult QuizResult { get; set; }

    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("type")]
    [JsonConverter(typeof(StringEnumConverter))]  
    [BsonRepresentation(BsonType.String)]
    public QuizType Type { get; set; }

    [BsonElement("rightAnswers")]
    public List<string> RightAnswers { get; set; }

    
}
