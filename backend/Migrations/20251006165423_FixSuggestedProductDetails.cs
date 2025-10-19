using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class FixSuggestedProductDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.AlterColumn<int>(
                name: "MaxStores",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxLayouts",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxLanguages",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { 0, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_SuggestedProductDetailId",
                table: "SuggestProducts",
                column: "SuggestedProductDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestProducts_ProductDetails_SuggestedProductDetailId",
                table: "SuggestProducts",
                column: "SuggestedProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuggestProducts_ProductDetails_SuggestedProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.DropIndex(
                name: "IX_SuggestProducts_SuggestedProductDetailId",
                table: "SuggestProducts");

            migrationBuilder.AlterColumn<int>(
                name: "MaxStores",
                table: "Packages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxLayouts",
                table: "Packages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaxLanguages",
                table: "Packages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MaxLanguages", "MaxLayouts", "MaxStores" },
                values: new object[] { null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestProducts_ProductDetailId",
                table: "SuggestProducts",
                column: "ProductDetailId",
                unique: true);
        }
    }
}
