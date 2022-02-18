using Microsoft.EntityFrameworkCore.Migrations;

namespace Wiser.API.Entities.Migrations
{
    public partial class addinstitutetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrincipalSign",
                table: "Institutes",
                newName: "Vision");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankIfsc",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mission",
                table: "Institutes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "BankIfsc",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Institutes");

            migrationBuilder.DropColumn(
                name: "Mission",
                table: "Institutes");

            migrationBuilder.RenameColumn(
                name: "Vision",
                table: "Institutes",
                newName: "PrincipalSign");
        }
    }
}
