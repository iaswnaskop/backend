using System;
using System.Collections.Generic;

namespace backend.Models;

public partial class ProductDetailModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
    public string? NameEng { get; set; }

    public string? DescriptionEng { get; set; }
    public string? NameDu { get; set; }

    public string? DescriptionDu { get; set; }
    public string? NameFr { get; set; }

    public string? DescriptionFr { get; set; }
    public string? NameIt { get; set; }

    public string? DescriptionIt { get; set; }
    public string? NameEs { get; set; }

    public string? DescriptionEs { get; set; }

    public string? ImageUrl { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }
    
    public bool IsHidden { get; set; } = false;
    public bool IsVegan { get; set; } = false;
    public bool GlutenFree { get; set; } = false;
    public bool IsKosher { get; set; } = false;
    public bool IsGluten { get; set; } = false;
    public bool IsSpicy { get; set; } = false;
    public bool ContainsNuts { get; set; } = false;
    public int? CategoryId { get; set; }
    public int StoreId { get; set; }
    public int ProductId { get; set; }
    //public IFormFile? ProductImage { get; set; }
    public List<int> SuggestedProduct { get; set; } = new List<int>();
}
