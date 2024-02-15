using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace QuizLib.Data.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum QuizСondition { Lower, Higher }
