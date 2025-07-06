using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixCourierOrderRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CourierProfiles_CourierProfileUserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_TraderProfiles_CourierId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourierProfileUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CourierProfileUserId",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CourierProfiles_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "CourierProfiles",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CourierProfiles_CourierId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CourierProfileUserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierProfileUserId",
                table: "Orders",
                column: "CourierProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CourierProfiles_CourierProfileUserId",
                table: "Orders",
                column: "CourierProfileUserId",
                principalTable: "CourierProfiles",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_TraderProfiles_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "TraderProfiles",
                principalColumn: "UserId");
        }
    }
}
