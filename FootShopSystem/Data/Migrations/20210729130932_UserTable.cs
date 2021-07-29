using Microsoft.EntityFrameworkCore.Migrations;

namespace FootShopSystem.Data.Migrations
{
    public partial class UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Shoes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchasesCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoesMade",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_UserId1",
                table: "Shoes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_AspNetUsers_UserId1",
                table: "Shoes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_AspNetUsers_UserId1",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_UserId1",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PurchasesCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ShoesMade",
                table: "AspNetUsers");
        }
    }
}
