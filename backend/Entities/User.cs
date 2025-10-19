namespace backend.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public string? PasswordHash { get; set; }
        public string? PasswordSetupToken { get; set; }
        public DateTime? TokenExpiresAt { get; set; }
        public string Phone { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public bool IsVerified { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool HasPassOnBoarding { get; set; } = false;
        public int PackageId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
        public virtual Packages? Package { get; set; }
        public virtual Role? Role { get; set; }

    }
}
