using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddDesignModelTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Δημιουργία πίνακα DesignModel
            migrationBuilder.CreateTable(
                name: "DesignModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignModel", x => x.Id);
                });

            // 2. Προσθήκη default εγγραφής στον DesignModel
            migrationBuilder.Sql("INSERT INTO DesignModel (Model) VALUES ('Default')");

            // 3. Προσθήκη στήλης DesignModelId στον Design με defaultValue = 1 (ή ID της εγγραφής)
            migrationBuilder.AddColumn<int>(
                name: "DesignModelId",
                table: "Design",
                type: "int",
                nullable: false,
                defaultValue: 1);

            // 4. Δημιουργία Index και Foreign Key
            migrationBuilder.CreateIndex(
                name: "IX_Design_DesignModelId",
                table: "Design",
                column: "DesignModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Design_DesignModel_DesignModelId",
                table: "Design",
                column: "DesignModelId",
                principalTable: "DesignModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Design_DesignModel_DesignModelId",
                table: "Design");

            migrationBuilder.DropIndex(
                name: "IX_Design_DesignModelId",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "DesignModelId",
                table: "Design");

            migrationBuilder.DropTable(
                name: "DesignModel");
        }
    }
}
