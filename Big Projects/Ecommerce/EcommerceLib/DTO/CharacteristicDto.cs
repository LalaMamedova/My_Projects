namespace EcommerceLib.DTO;

public record CharacteristicDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int SubCategoryId { get; set; }
}
