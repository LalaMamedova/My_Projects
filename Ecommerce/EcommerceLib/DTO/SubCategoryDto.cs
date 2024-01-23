namespace EcommerceLib.DTO;

public record SubCategoryDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string Icon { get; set; }
    public ICollection<CharacteristicDto>? Characteristics { get; set; }
}
