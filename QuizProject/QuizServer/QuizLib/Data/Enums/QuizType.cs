using Newtonsoft.Json;

namespace QuizLib.Data.Enums;

[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum QuizType { Learning, ForFun }
