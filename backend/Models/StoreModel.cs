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
    public string AFM { get; set; }
    public string? ImageURL { get; set; }
    public string? Email { get; set; }
    public string? Facebook { get; set; }
    public string? Instagram { get; set; }
    public string? TikTok { get; set; }
    public string? TripAdvisor { get; set; }
    public string? GoogleBusiness { get; set; }

    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();

    public virtual ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    public virtual ICollection<TypeModel> TypeModel { get; set; } = new List<TypeModel>();
}
