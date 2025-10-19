namespace backend.Entities
{
    public class PackagePermission
    {
        public int PackageId { get; set; }
        public Packages Package { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
