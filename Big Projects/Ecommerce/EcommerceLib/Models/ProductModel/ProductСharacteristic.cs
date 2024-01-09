
namespace EcommerceLib.Models.ProductModel;

public class ProductСharacteristic
{
    public int Id { get; set; }

    public int CharacteristicId { get; set; }
    public Characteristic Characteristic { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public string Value { get; set; }
}
