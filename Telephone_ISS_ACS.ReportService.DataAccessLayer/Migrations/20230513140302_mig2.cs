using Microsoft.EntityFrameworkCore.Migrations;

namespace Telephone_ISS_ACS.ReportService.DataAccessLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Report");

            migrationBuilder.AddColumn<int>(
                name: "UserCount",
                table: "Report",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCount",
                table: "Report");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Report",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Report",
                type: "text",
                nullable: true);
        }
    }
}
