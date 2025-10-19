using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixSuggestedProductDetailsv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductDetailId_SuggestedProductDetailId",
                table: "SuggestProducts",
                columns: new[] { "ProductDetailId", "SuggestedProductDetailId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_ProductDetailId_SuggestedProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts",
                column: "ProductDetailId");
        }
    }
}
