using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationAppDemo.Migrations
{
    public partial class _6_CreateCampaignStatisticsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignStatistics",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    TotalDonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTransferredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalExpendedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignStatistics", x => x.CampaignId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignStatistics");
        }
    }
}
