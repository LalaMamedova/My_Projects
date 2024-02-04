using System.Collections.Generic;

namespace TestsLib.Dto;

public class TestDto
{
    public string? Id { get; set; } 
    public string Title { get; set; }
    public string Description { get; set; }
    public string? TitleImg { get; set; }
    public string UserId { get; set; }
    public List<string> Tags { get; set; }

    public List<TestQuestionDto> Questions { get; set; }
}
