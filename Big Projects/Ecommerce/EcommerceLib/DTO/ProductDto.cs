
namespace EcommerceLib.DTO;


public record ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }

    public int BrandId { get; set; }
    public int SubCategoryId { get; set; }

    public ICollection<ProductImgDto>? ProductsImg  { get; set; }
    public ICollection<ProductOriginalImgDto>? OriginalImgs { get; set; }
    public ICollection<ProductСharacteristicDto>? ProductСharacteristics { get; set; }
}
