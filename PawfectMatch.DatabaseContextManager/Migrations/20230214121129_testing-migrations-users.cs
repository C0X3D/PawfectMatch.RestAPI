using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.DatabaseContextManager.Migrations
{
    public partial class testingmigrationsusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetType",
                columns: table => new
                {
                    PetTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetType", x => x.PetTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PetBreed",
                columns: table => new
                {
                    PetBreedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetClassId = table.Column<int>(type: "int", nullable: false),
                    BreedName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreedTypePetTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetBreed", x => x.PetBreedId);
                    table.ForeignKey(
                        name: "FK_PetBreed_PetType_BreedTypePetTypeId",
                        column: x => x.BreedTypePetTypeId,
                        principalTable: "PetType",
                        principalColumn: "PetTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    PetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PetBreedId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.PetId);
                    table.ForeignKey(
                        name: "FK_Pet_PetBreed_PetBreedId",
                        column: x => x.PetBreedId,
                        principalTable: "PetBreed",
                        principalColumn: "PetBreedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pet_Users_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pet_ApplicationUserId",
                table: "Pet",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pet_PetBreedId",
                table: "Pet",
                column: "PetBreedId");

            migrationBuilder.CreateIndex(
                name: "IX_PetBreed_BreedTypePetTypeId",
                table: "PetBreed",
                column: "BreedTypePetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pet");

            migrationBuilder.DropTable(
                name: "PetBreed");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PetType");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
