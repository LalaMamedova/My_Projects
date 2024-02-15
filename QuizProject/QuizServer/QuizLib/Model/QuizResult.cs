using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuizLib.Data.Enums;


namespace QuizApp.Model;

public class QuizResult
{
    [BsonElement("resultDescription")]
    public string ResultDescription { get; set; }

    [BsonElement("resultTitle")]
    public string ResultTitle { get; set; }

    [BsonElement("condiditon")]
    [BsonRepresentation(BsonType.String)]
    public QuizСondition Condition { get; set; }

    [BsonElement("condiditonValue")]
    public int ConditionValue { get; set; }

}
