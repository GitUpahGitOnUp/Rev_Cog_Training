using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankLIB.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceShadowFkWithAccNo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Accounts_AccountsAccNo",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountsAccNo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountsAccNo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_AccountsAccNo",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "AccountsAccNo",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountsAccNo",
                table: "ServiceRequests");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccNo",
                table: "Transactions",
                column: "AccNo");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_AccNo",
                table: "ServiceRequests",
                column: "AccNo");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Accounts_AccNo",
                table: "ServiceRequests",
                column: "AccNo",
                principalTable: "Accounts",
                principalColumn: "AccNo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccNo",
                table: "Transactions",
                column: "AccNo",
                principalTable: "Accounts",
                principalColumn: "AccNo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Accounts_AccNo",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccNo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccNo",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_AccNo",
                table: "ServiceRequests");

            migrationBuilder.AddColumn<int>(
                name: "AccountsAccNo",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountsAccNo",
                table: "ServiceRequests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountsAccNo",
                table: "Transactions",
                column: "AccountsAccNo");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_AccountsAccNo",
                table: "ServiceRequests",
                column: "AccountsAccNo");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Accounts_AccountsAccNo",
                table: "ServiceRequests",
                column: "AccountsAccNo",
                principalTable: "Accounts",
                principalColumn: "AccNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountsAccNo",
                table: "Transactions",
                column: "AccountsAccNo",
                principalTable: "Accounts",
                principalColumn: "AccNo");
        }
    }
}
