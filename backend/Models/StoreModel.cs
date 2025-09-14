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

    public string? PurchasingManager { get;  set; }
    public string? WifiName { get;  set; }
    public string? WifiPassword { get;  set; }
    public string? StorePhone { get;  set; }
    public string? QRCodeURL { get; set; }
    public bool? Cash { get; set; }
    public bool? Card { get; set; }
    public bool? PayPal { get; set; }
    public bool? BitCoin { get; set; }
    public bool? IRIS { get; set; }
    public bool? IsActive { get; set; }
    public virtual ICollection<ProductDetailModel> ProductDetails { get; set; } = new List<ProductDetailModel>();

    public virtual ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    public virtual ICollection<TypeModel> StoreType { get; set; } = new List<TypeModel>();
    public virtual ICollection<LanguageModel> Languages { get; set; } = new List<LanguageModel>();
   // public virtual ICollection<DesignModel> DesignModel { get; set; } = new List<DesignModel>();// DesignModels
    public virtual ICollection<GetDesign> Design { get; set; } = new List<GetDesign>();// DesignModels
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    public virtual ICollection<PromoModel> Promo { get; set; } = new List<PromoModel>();

    //public List<ScheduleModel> ScheduleModel { get; set; } = new List<ScheduleModel>();

}

