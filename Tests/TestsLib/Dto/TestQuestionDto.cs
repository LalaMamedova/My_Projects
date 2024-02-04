
namespace TestsLib.Dto;

public class TestQuestionDto
{
    public string? Id { get; set; }
    public string? Answer { get; set; }
    public string QuestionDescription { get; set; }
    public string TestId { get; set; }
    public List<string> Choice { get; set; }
}
