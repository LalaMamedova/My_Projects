using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model;

public class QuizResult
{
    [BsonElement("resultDescription")]
    public List<string> ResultDescription { get; set; }

    [BsonElement("resultTitle")]
    public List<string> ResultTitle { get; set; }

}
