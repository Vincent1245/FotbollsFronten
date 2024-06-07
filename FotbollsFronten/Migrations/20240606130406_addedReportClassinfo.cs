using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FotbollsFronten.Migrations
{
    /// <inheritdoc />
    public partial class addedReportClassinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReportedUserName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportedUserName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Reports");
        }
    }
}
