using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class CategoryModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    public string? NameEng { get; set; }
    public string? DescriptionEng { get; set; }
    public string? NameDu { get; set; }
    public string? DescriptionDu { get; set; }
    public string? NameFr { get; set; }
    public string? DescriptionFr { get; set; }
    public string? NameIt { get; set; }
    public string? DescriptionIt { get; set; }
    public string?  NameEs { get; set; }
    public string? DescriptionEs { get; set; }
    public int StoreId { get; set; }

    //public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();

    //public virtual ICollection<StoreModel> Stores { get; set; } = new List<StoreModel>();
}
