using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingAPI.Migrations
{
    /// <inheritdoc />
    public partial class edit_financial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransfers_Banks_DestinationId",
                table: "FinancialTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransfers_Banks_SourceId",
                table: "FinancialTransfers");

            migrationBuilder.DropColumn(
                name: "DestinationType",
                table: "FinancialTransfers");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "FinancialTransfers");

            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "FinancialTransfers",
                newName: "SourceBankId");

            migrationBuilder.RenameColumn(
                name: "DestinationId",
                table: "FinancialTransfers",
                newName: "DestinationBankId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransfers_SourceId",
                table: "FinancialTransfers",
                newName: "IX_FinancialTransfers_SourceBankId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransfers_DestinationId",
                table: "FinancialTransfers",
                newName: "IX_FinancialTransfers_DestinationBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransfers_Banks_DestinationBankId",
                table: "FinancialTransfers",
                column: "DestinationBankId",
                principalTable: "Banks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransfers_Banks_SourceBankId",
                table: "FinancialTransfers",
                column: "SourceBankId",
                principalTable: "Banks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransfers_Banks_DestinationBankId",
                table: "FinancialTransfers");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransfers_Banks_SourceBankId",
                table: "FinancialTransfers");

            migrationBuilder.RenameColumn(
                name: "SourceBankId",
                table: "FinancialTransfers",
                newName: "SourceId");

            migrationBuilder.RenameColumn(
                name: "DestinationBankId",
                table: "FinancialTransfers",
                newName: "DestinationId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransfers_SourceBankId",
                table: "FinancialTransfers",
                newName: "IX_FinancialTransfers_SourceId");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransfers_DestinationBankId",
                table: "FinancialTransfers",
                newName: "IX_FinancialTransfers_DestinationId");

            migrationBuilder.AddColumn<string>(
                name: "DestinationType",
                table: "FinancialTransfers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceType",
                table: "FinancialTransfers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransfers_Banks_DestinationId",
                table: "FinancialTransfers",
                column: "DestinationId",
                principalTable: "Banks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransfers_Banks_SourceId",
                table: "FinancialTransfers",
                column: "SourceId",
                principalTable: "Banks",
                principalColumn: "Id");
        }
    }
}
