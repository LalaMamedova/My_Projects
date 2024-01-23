namespace EcommerceLib.DTO;

public class ReviewDto
{

    public int Id { get; set; }
    public float Rating { get; set; }
    public string AppUserId { get; set; }
    public int ProductId { get; set; }
}
