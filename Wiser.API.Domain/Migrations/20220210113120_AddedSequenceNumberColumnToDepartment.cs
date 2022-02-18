using Microsoft.EntityFrameworkCore.Migrations;

namespace Wiser.API.Entities.Migrations
{
    public partial class AddedSequenceNumberColumnToDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SequenceNumber",
                table: "DeparmentSection",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SequenceNumber",
                table: "DeparmentSection");
        }
    }
}
