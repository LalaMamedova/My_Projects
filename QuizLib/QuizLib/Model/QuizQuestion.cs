using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Model;

public class QuizQuestion
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("question")]
    public string Question { get; set; }
     
    [BsonElement("answer")]
    public string Answer { get; set; }

    [BsonElement("options")]
    public List<string> Options { get; set; }
}
