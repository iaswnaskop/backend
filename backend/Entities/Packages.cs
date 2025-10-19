namespace backend.Entities
{
    public class Packages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public int MaxStores { get; set; } 
        public int MaxLayouts { get; set; } 
        public int MaxLanguages { get; set; }
        public ICollection<PackagePermission> PackagePermissions { get; set; } = new List<PackagePermission>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
