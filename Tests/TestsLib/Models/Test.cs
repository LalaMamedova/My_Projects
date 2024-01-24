using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsLib.Models;
public class Test
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } 

    [BsonElement("title")]
    public string Title {  get; set; }

    [BsonElement("titleImg")]
    public string TitleImg { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("questions")]
    public List<TestQuestion> Questions { get; set; }

    [BsonElement("passedCount")]
    [BsonDefaultValue(0)]
    public int PassedCount { get; set; }
  
   
}
