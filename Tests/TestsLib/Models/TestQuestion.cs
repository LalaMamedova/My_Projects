using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TestsLib.Models;

public class TestQuestion
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]   
    public ObjectId Id { get; set; }

    [BsonElement("answer")]
    [BsonDefaultValue(null)]
    public string? Answer { get; set; };

    [BsonElement("questionDescription")]
    public string QuestionDescription { get; set; }

    [BsonElement("choice")]
    public string[] Choice { get; set; }


}
