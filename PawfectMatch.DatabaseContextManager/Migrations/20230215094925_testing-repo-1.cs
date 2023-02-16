using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawfectMatch.DatabaseContextManager.Migrations
{
    public partial class testingrepo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfile_ProfileId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfile_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserProfile_ProfileId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ProfileId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserProfile_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
