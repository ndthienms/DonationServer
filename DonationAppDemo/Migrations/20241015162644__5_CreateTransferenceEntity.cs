using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationAppDemo.Migrations
{
    public partial class _5_CreateTransferenceEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1000)", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    AdminId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transference", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transference_AdminId",
                table: "Transference",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Transference_CampaignId",
                table: "Transference",
                column: "CampaignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transference");
        }
    }
}
