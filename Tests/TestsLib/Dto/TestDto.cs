namespace TestsLib.Dto;

public class TestDto
{
    public string? Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public string? TitleImg { get; set; }
    public string[] Tags { get; set; }
    public string UserId { get; set; }

    public ICollection<TestQuestionDto> Question { get; set; }
}
