using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class PromosSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Promos");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Promos");

            migrationBuilder.CreateTable(
                name: "SuggestProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDetailId = table.Column<int>(type: "int", nullable: false),
                    SuggestedProductDetailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestProducts_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts",
                column: "ProductDetailId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestProducts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Promos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Promos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
