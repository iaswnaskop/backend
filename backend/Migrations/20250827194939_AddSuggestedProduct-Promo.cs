using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSuggestedProductPromo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Design_DesignId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DesignId",
                table: "Stores");

            migrationBuilder.AddColumn<string>(
                name: "QRCodeURL",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ContainsNuts",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GlutenFree",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHidden",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsKosher",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpicy",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegan",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Design",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Promos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Promos_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Design_StoreId",
                table: "Design",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Promos_StoreId",
                table: "Promos",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Design_Stores_StoreId",
                table: "Design",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Stores_StoreId",
                table: "Design");

            migrationBuilder.DropTable(
                name: "Promos");

            migrationBuilder.DropIndex(
                name: "IX_Design_StoreId",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "QRCodeURL",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "ContainsNuts",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "GlutenFree",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsHidden",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsKosher",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsSpicy",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsVegan",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Design");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DesignId",
                table: "Stores",
                column: "DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Design_DesignId",
                table: "Stores",
                column: "DesignId",
                principalTable: "Design",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
