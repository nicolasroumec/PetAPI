using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsVerifiedToShelter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isApproved",
                table: "Shelters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isApproved",
                table: "Shelters");
        }
    }
}
