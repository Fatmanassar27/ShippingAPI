using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminToFinancialTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "FinancialTransfers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransfers_AdminId",
                table: "FinancialTransfers",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransfers_AspNetUsers_AdminId",
                table: "FinancialTransfers",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransfers_AspNetUsers_AdminId",
                table: "FinancialTransfers");

            migrationBuilder.DropIndex(
                name: "IX_FinancialTransfers_AdminId",
                table: "FinancialTransfers");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "FinancialTransfers");
        }
    }
}
