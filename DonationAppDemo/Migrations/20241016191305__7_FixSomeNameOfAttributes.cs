using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationAppDemo.Migrations
{
    public partial class _7_FixSomeNameOfAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "RateCampaign",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "disabled",
                table: "Post",
                newName: "Disabled");

            migrationBuilder.RenameColumn(
                name: "method",
                table: "PaymentMethod",
                newName: "Method");

            migrationBuilder.RenameColumn(
                name: "disabled",
                table: "Campaign",
                newName: "Disabled");

            migrationBuilder.RenameColumn(
                name: "disabled",
                table: "Account",
                newName: "Disabled");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "RateCampaign",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "Disabled",
                table: "Post",
                newName: "disabled");

            migrationBuilder.RenameColumn(
                name: "Method",
                table: "PaymentMethod",
                newName: "method");

            migrationBuilder.RenameColumn(
                name: "Disabled",
                table: "Campaign",
                newName: "disabled");

            migrationBuilder.RenameColumn(
                name: "Disabled",
                table: "Account",
                newName: "disabled");
        }
    }
}
