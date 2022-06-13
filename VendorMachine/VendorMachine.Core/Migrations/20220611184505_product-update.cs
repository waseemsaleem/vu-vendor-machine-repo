using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendorMachine.Core.Migrations
{
    public partial class productupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerIdUserId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellerIdUserId",
                table: "Products",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerIdUserId",
                table: "Products",
                newName: "IX_Products_SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_SellerId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Products",
                newName: "SellerIdUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SellerId",
                table: "Products",
                newName: "IX_Products_SellerIdUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_SellerIdUserId",
                table: "Products",
                column: "SellerIdUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
