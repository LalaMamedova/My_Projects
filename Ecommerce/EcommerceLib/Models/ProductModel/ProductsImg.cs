namespace EcommerceLib.Models.ProductModel;


public class ProductsImg
{
    public int Id { get; set; }

    public int ProductId { get; set; }  
    public Product Product { get; set; }

    public string ImagePath { get; set; }
}
