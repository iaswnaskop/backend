using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTranslate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionDu",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEng",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEs",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionFr",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionIt",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameDu",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEng",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEs",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFr",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameIt",
                table: "ProductDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionDu",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "DescriptionEng",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "DescriptionEs",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "DescriptionFr",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "DescriptionIt",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "NameDu",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "NameEng",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "NameEs",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "NameFr",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "NameIt",
                table: "ProductDetails");
        }
    }
}
