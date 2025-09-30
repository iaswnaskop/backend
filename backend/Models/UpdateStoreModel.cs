using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class UpdateStoreForm
    {
        [FromForm(Name = "JsonData")]
        public string JsonData { get; set; } = string.Empty;

        [FromForm(Name = "Logo")]
        public IFormFile? Logo { get; set; }
    }

    public class UpdateStoreModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("afm")]
        public required string AFM { get; set; }

        [JsonPropertyName("imageURL")]
        public string? ImageURL { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("facebook")]
        public string? Facebook { get; set; }

        [JsonPropertyName("instagram")]
        public string? Instagram { get; set; }

        [JsonPropertyName("tiktok")]
        public string? TikTok { get; set; }

        [JsonPropertyName("tripAdvisor")]
        public string? TripAdvisor { get; set; }

        [JsonPropertyName("googleBusiness")]
        public string? GoogleBusiness { get; set; }

        [JsonPropertyName("purchasingManager")]
        public string? PurchasingManager { get; set; }

        [JsonPropertyName("wifiName")]
        public string? WifiName { get; set; }

        [JsonPropertyName("wifiPassword")]
        public string? WifiPassword { get; set; }

        [JsonPropertyName("storePhone")]
        public string? StorePhone { get; set; }

        [JsonPropertyName("qrCodeURL")]
        public string? QRCodeURL { get; set; }

        [JsonPropertyName("cash")]
        public bool? Cash { get; set; }

        [JsonPropertyName("card")]
        public bool? Card { get; set; }

        [JsonPropertyName("payPal")]
        public bool? PayPal { get; set; }

        [JsonPropertyName("bitCoin")]
        public bool? BitCoin { get; set; }

        [JsonPropertyName("iris")]
        public bool? IRIS { get; set; }

        [JsonPropertyName("isActive")]
        public bool? IsActive { get; set; }

        [JsonPropertyName("languages")]
        public List<int> Languages { get; set; } = new List<int>();

        [JsonPropertyName("scheduleModel")]
        public List<ScheduleModel> ScheduleModel { get; set; } = new List<ScheduleModel>();

        [JsonPropertyName("logo")]
        public IFormFile? Logo { get; set; }
    }

    public class ScheduleModel
    {
        [JsonPropertyName("dayOfWeek")]
        public int DayOfWeek { get; set; }

        [JsonPropertyName("openingTime")]
        public string OpeningTime { get; set; }

        [JsonPropertyName("closingTime")]
        public string ClosingTime { get; set; }

        [JsonPropertyName("isClosed")]
        public bool IsClosed { get; set; } = false;
    }
}
