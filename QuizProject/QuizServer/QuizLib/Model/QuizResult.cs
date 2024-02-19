using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace QuizApp.Model;

public class QuizResult
{
    [BsonElement("resultDescription")]
    public string ResultDescription { get; set; }

    [BsonElement("resultTitle")]
    public string ResultTitle { get; set; }

    //[BsonElement("condiditon")]
    //[BsonRepresentation(BsonType.String)]
    //public QuizСondition? Condition { get; set; }

    [BsonElement("condiditonValueFrom")]
    public int ConditionValueFrom { get; set; }

    [BsonElement("condiditonValueTo")]
    public int ConditionValueTo { get; set; }
}
