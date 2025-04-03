using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOpeningHoursFromShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "openingHours",
                table: "Shelters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "openingHours",
                table: "Shelters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
