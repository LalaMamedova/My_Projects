using EcommerceLib.Models.UserModel.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceLib.Models.ProductModel;

public class PurchasedProduct
{
    public int Id {  get; set; }    
    public AppUser AppUser { get; set; }
    public string AppUserId {  get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public float TotalSum {  get; set; }    
}
