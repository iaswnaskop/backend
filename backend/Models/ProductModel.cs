using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductModel
{
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? NameEng { get; set; }
    public string? NameDu { get; set; }
    public string? NameFr { get; set; }
    public string? NameIt { get; set; }
    public string? NameEs { get; set; }

    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();
}
