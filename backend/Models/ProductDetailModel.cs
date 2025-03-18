using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductDetailModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public int StoreId { get; set; }

    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public virtual CategoryModel? Category { get; set; }

    public virtual ProductModel Product { get; set; } = null!;

    public virtual StoreModel Store { get; set; } = null!;
}
