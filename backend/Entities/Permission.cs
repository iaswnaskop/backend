namespace backend.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<PackagePermission> PackagePermissions { get; set; } = new List<PackagePermission>();
    }
}
