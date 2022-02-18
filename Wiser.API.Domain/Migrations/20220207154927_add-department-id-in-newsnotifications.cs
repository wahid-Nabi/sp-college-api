using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wiser.API.Entities.Migrations
{
    public partial class adddepartmentidinnewsnotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "NewsNotifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsNotifications_DepartmentId",
                table: "NewsNotifications",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsNotifications_Department_DepartmentId",
                table: "NewsNotifications",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsNotifications_Department_DepartmentId",
                table: "NewsNotifications");

            migrationBuilder.DropIndex(
                name: "IX_NewsNotifications_DepartmentId",
                table: "NewsNotifications");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "NewsNotifications");
        }
    }
}
