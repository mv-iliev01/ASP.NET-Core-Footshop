using Microsoft.EntityFrameworkCore.Migrations;

namespace FootShopSystem.Data.Migrations
{
    public partial class FavouritesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Categories_CategoryId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Designers_DesignerId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Sizes_SizeId",
                table: "Shoes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Designers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => new { x.ShoeId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Favourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Designers_UserId",
                table: "Designers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Designers_AspNetUsers_UserId",
                table: "Designers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Categories_CategoryId",
                table: "Shoes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Designers_DesignerId",
                table: "Shoes",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Sizes_SizeId",
                table: "Shoes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designers_AspNetUsers_UserId",
                table: "Designers");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Categories_CategoryId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Designers_DesignerId",
                table: "Shoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_Sizes_SizeId",
                table: "Shoes");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Designers_UserId",
                table: "Designers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Designers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Categories_CategoryId",
                table: "Shoes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Colors_ColorId",
                table: "Shoes",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Designers_DesignerId",
                table: "Shoes",
                column: "DesignerId",
                principalTable: "Designers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_Sizes_SizeId",
                table: "Shoes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
