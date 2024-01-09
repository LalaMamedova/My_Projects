using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceLib.Models.ProductModel;

public class ProductOriginalImg 
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string OriginalImgPath { get; set; }

}
