using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DonationAppDemo.Migrations
{
    public partial class _2_ChangePrimaryKeyToAccount_PhoneNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Post",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Organiser",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Notification",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Donor",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "CommentPost",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Admin",
                type: "varchar(11)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNum",
                table: "Account",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "PhoneNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Organiser",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Notification",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Donor",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "CommentPost",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Admin",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNum",
                table: "Account",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");
        }
    }
}
