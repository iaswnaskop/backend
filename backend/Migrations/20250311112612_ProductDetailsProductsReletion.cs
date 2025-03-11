using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ProductDetailsProductsReletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Stores_StoreIdId",
                table: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "StoreIdId",
                table: "ProductDetails",
                newName: "StoreId");

            migrationBuilder.RenameColumn(
                name: "Available",
                table: "ProductDetails",
                newName: "IsAvailable");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_StoreIdId",
                table: "ProductDetails",
                newName: "IX_ProductDetails_StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Stores_StoreId",
                table: "ProductDetails",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDetails_Stores_StoreId",
                table: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "ProductDetails",
                newName: "StoreIdId");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "ProductDetails",
                newName: "Available");

            migrationBuilder.RenameIndex(
                name: "IX_ProductDetails_StoreId",
                table: "ProductDetails",
                newName: "IX_ProductDetails_StoreIdId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Stores_StoreIdId",
                table: "ProductDetails",
                column: "StoreIdId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
