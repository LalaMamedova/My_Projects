using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace QuizLib.Data;

[JsonConverter(typeof(StringEnumConverter))]
public enum Condition { Lower, Higher }


[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum QuizType { Learning, ForFun, Psychological }



[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum OptionFormat { OneRightAnswer, ManyRightAnswer, Scores }
