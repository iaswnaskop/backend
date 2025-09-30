using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class AddProductDeatilForm
    {
        [FromForm(Name = "JsonData")]
        public string JsonData { get; set; }
        [FromForm(Name ="ProductImage")]
        public IFormFile? ProductImage { get; set; }
    }
    public class AddProductDetailModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("nameEng")]
        public string? NameEng { get; set; }
        [JsonPropertyName("descriptionEng")]
        public string? DescriptionEng { get; set; }
        [JsonPropertyName("nameDu")]
        public string? NameDu { get; set; }
        [JsonPropertyName("descriptionDu")]
        public string? DescriptionDu { get; set; }
        [JsonPropertyName("nameFr")]
        public string? NameFr { get; set; }
        [JsonPropertyName("descriptionFr")]
        public string? DescriptionFr { get; set; }
        [JsonPropertyName("nameIt")]
        public string? NameIt { get; set; }
        [JsonPropertyName("descriptionIt")]
        public string? DescriptionIt { get; set; }
        [JsonPropertyName("nameEs")]
        public string? NameEs { get; set; }
        [JsonPropertyName("descriptionEs")]
        public string? DescriptionEs { get; set; }
        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("available")]
        public bool Available { get; set; }
        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; } = false;
        [JsonPropertyName("isVegan")]
        public bool IsVegan { get; set; } = false;
        [JsonPropertyName("glutenFree")]
        public bool GlutenFree { get; set; } = false;
        [JsonPropertyName("isKosher")]
        public bool IsKosher { get; set; } = false;
        [JsonPropertyName("isGluten")]
        public bool IsGluten { get; set; } = false;
        [JsonPropertyName("isSpicy")]
        public bool IsSpicy { get; set; } = false;
        [JsonPropertyName("containsNuts")]
        public bool ContainsNuts { get; set; } = false; 
        [JsonPropertyName("suggestedProduct")]
        public List<int> SuggestedProduct { get; set; } = new List<int>();
    }
}
