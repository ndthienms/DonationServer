using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationAppDemo.Migrations
{
    public partial class _3_AddEntity_AddSomeAttributesInEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.AddColumn<string>(
                name: "PaymentDescription",
                table: "Donation",
                type: "nvarchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Donation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentOrderId",
                table: "Donation",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentResponse",
                table: "Donation",
                type: "varchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentToken",
                table: "Donation",
                type: "varchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTransactionId",
                table: "Donation",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverSrc",
                table: "Campaign",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverSrcPublicId",
                table: "Campaign",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    ExpenseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    OrganiserId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    method = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donation_PaymentMethodId",
                table: "Donation",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CampaignId",
                table: "Expense",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_OrganiserId",
                table: "Expense",
                column: "OrganiserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_Donation_PaymentMethodId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentDescription",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentOrderId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentResponse",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentToken",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "PaymentTransactionId",
                table: "Donation");

            migrationBuilder.DropColumn(
                name: "CoverSrc",
                table: "Campaign");

            migrationBuilder.DropColumn(
                name: "CoverSrcPublicId",
                table: "Campaign");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: true),
                    OrganiserId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_CampaignId",
                table: "Payment",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_OrganiserId",
                table: "Payment",
                column: "OrganiserId");
        }
    }
}
