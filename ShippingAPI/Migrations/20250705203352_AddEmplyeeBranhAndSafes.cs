using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmplyeeBranhAndSafes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserPermissions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeBranches",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBranches", x => new { x.UserId, x.BranchId });
                    table.ForeignKey(
                        name: "FK_EmployeeBranches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeeBranches_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSafes",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SafeId = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSafes", x => new { x.UserId, x.SafeId });
                    table.ForeignKey(
                        name: "FK_EmployeeSafes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeeSafes_Safes_SafeId",
                        column: x => x.SafeId,
                        principalTable: "Safes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_ApplicationUserId",
                table: "UserPermissions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBranches_BranchId",
                table: "EmployeeBranches",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSafes_SafeId",
                table: "EmployeeSafes",
                column: "SafeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPermissions_AspNetUsers_ApplicationUserId",
                table: "UserPermissions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPermissions_AspNetUsers_ApplicationUserId",
                table: "UserPermissions");

            migrationBuilder.DropTable(
                name: "EmployeeBranches");

            migrationBuilder.DropTable(
                name: "EmployeeSafes");

            migrationBuilder.DropIndex(
                name: "IX_UserPermissions_ApplicationUserId",
                table: "UserPermissions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserPermissions");
        }
    }
}
