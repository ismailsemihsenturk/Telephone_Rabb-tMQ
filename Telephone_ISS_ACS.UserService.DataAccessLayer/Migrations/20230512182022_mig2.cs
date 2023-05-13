using Microsoft.EntityFrameworkCore.Migrations;

namespace Telephone_ISS_ACS.UserService.DataAccessLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformation_PhoneBookEntry_PhoneBookEntryId",
                table: "ContactInformation");

            migrationBuilder.AddForeignKey(
                name: "PhoneBookEntry_Id",
                table: "ContactInformation",
                column: "PhoneBookEntryId",
                principalTable: "PhoneBookEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "PhoneBookEntry_Id",
                table: "ContactInformation");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformation_PhoneBookEntry_PhoneBookEntryId",
                table: "ContactInformation",
                column: "PhoneBookEntryId",
                principalTable: "PhoneBookEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
