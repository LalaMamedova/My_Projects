using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using QuizLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizLib.Model;

public class QuizQuestion
{

    [BsonElement("question")]
    public string Question { get; set; }
     
    [BsonElement("answer")]
    public string Answer { get; set; }

    [BsonElement("options")]
    public List<string> Options { get; set; }

    [BsonElement("rightAnswers")]
    public ICollection<string>? RightAnswers { get; set; }

    [BsonElement("optionFormat")]
    public OptionFormat OptionFormats { get; set; }   

}
