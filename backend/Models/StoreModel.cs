using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class StoreModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();

    public virtual ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
}
