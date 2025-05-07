using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDesignStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Βήμα 1: Κατάργηση σχέσης Design -> Store
            migrationBuilder.DropForeignKey(
                name: "FK_Design_Stores_StoreId",
                table: "Design");

            migrationBuilder.DropIndex(
                name: "IX_Design_StoreId",
                table: "Design");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Design");

            // Βήμα 2: Εισαγωγή default εγγραφής στο Design για να χρησιμοποιηθεί ως foreign key
            migrationBuilder.Sql("INSERT INTO Design DEFAULT VALUES");

            // Βήμα 3: Προσθήκη της νέας σχέσης Store -> Design με DesignId NOT NULL
            migrationBuilder.AddColumn<int>(
                name: "DesignId",
                table: "Stores",
                type: "int",
                nullable: false,
                defaultValue: 1); // <-- Το Id της εγγραφής που μόλις προστέθηκε

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Βήμα 1: Κατάργηση σχέσης Store -> Design
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Design_DesignId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DesignId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DesignId",
                table: "Stores");

            // Βήμα 2: Επαναφορά της σχέσης Design -> Store
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Design",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Design_StoreId",
                table: "Design",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Design_Stores_StoreId",
                table: "Design",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
