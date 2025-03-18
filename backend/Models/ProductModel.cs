using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();
}
