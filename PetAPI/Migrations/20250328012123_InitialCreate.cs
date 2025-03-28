using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dni = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shelter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    openingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shelter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shelter_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    shelterId = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donation_Shelter_shelterId",
                        column: x => x.shelterId,
                        principalTable: "Shelter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donation_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    healthStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    photos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    goodWithKids = table.Column<bool>(type: "bit", nullable: false),
                    goodWithPets = table.Column<bool>(type: "bit", nullable: false),
                    ShelterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pet_Shelter_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShelterSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shelterId = table.Column<int>(type: "int", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false),
                    openingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    closingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    isClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelterSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShelterSchedule_Shelter_shelterId",
                        column: x => x.shelterId,
                        principalTable: "Shelter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    petId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_Pet_petId",
                        column: x => x.petId,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionRequest_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_petId",
                table: "AdoptionRequest",
                column: "petId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_userId",
                table: "AdoptionRequest",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_shelterId",
                table: "Donation",
                column: "shelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_userId",
                table: "Donation",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_ShelterId",
                table: "Pet",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Shelter_UserId",
                table: "Shelter",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterSchedule_shelterId",
                table: "ShelterSchedule",
                column: "shelterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptionRequest");

            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "ShelterSchedule");

            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "Shelter");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
