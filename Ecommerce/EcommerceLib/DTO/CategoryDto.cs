
namespace EcommerceLib.DTO;

public record CategoryDto
{
    public int Id { get; set; } 
    public string Name { get; set; }
    public string Icon { get; set; }    
    public ICollection<SubCategoryDto> SubCategories { get; set; }
}
