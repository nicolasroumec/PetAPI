using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateVerificationCodesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shelterSchedules_Shelters_shelterId",
                table: "shelterSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shelterSchedules",
                table: "shelterSchedules");

            migrationBuilder.RenameTable(
                name: "shelterSchedules",
                newName: "ShelterSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_shelterSchedules_shelterId",
                table: "ShelterSchedules",
                newName: "IX_ShelterSchedules_shelterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShelterSchedules",
                table: "ShelterSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShelterSchedules_Shelters_shelterId",
                table: "ShelterSchedules",
                column: "shelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShelterSchedules_Shelters_shelterId",
                table: "ShelterSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShelterSchedules",
                table: "ShelterSchedules");

            migrationBuilder.RenameTable(
                name: "ShelterSchedules",
                newName: "shelterSchedules");

            migrationBuilder.RenameIndex(
                name: "IX_ShelterSchedules_shelterId",
                table: "shelterSchedules",
                newName: "IX_shelterSchedules_shelterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_shelterSchedules",
                table: "shelterSchedules",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shelterSchedules_Shelters_shelterId",
                table: "shelterSchedules",
                column: "shelterId",
                principalTable: "Shelters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
