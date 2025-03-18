using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CategoryModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();

    public virtual ICollection<StoreModel> Stores { get; set; } = new List<StoreModel>();
}
