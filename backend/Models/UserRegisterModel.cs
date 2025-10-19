using System.Text.Json.Serialization;

namespace backend.Models
{
    public class WooOrderEvent
    {
        public string Event { get; set; } = string.Empty;
        public Order Order { get; set; } = new();
        public Customer Customer { get; set; } = new();
        public Payment Payment { get; set; } = new();
        public List<Plan> Plans { get; set; } = new();
        public Metadata Metadata { get; set; } = new();
    }

    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string PaidAt { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public double AmountTotal { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class Customer
    {
        public int? WpUserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Role { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }

    public class Payment
    {
        public string Gateway { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string MethodTitle { get; set; } = string.Empty;
    }

    public class Plan
    {
        public string Code { get; set; } = string.Empty;
        public int Qty { get; set; }
    }

    public class Metadata
    {
        public string Site { get; set; } = string.Empty;
    }

    

}
