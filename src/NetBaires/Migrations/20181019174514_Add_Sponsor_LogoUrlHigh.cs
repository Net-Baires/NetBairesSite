using Microsoft.EntityFrameworkCore.Migrations;

namespace NetBaires.Migrations
{
    public partial class Add_Sponsor_LogoUrlHigh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Sponsors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrlHigh",
                table: "Sponsors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Sponsors");

            migrationBuilder.DropColumn(
                name: "LogoUrlHigh",
                table: "Sponsors");
        }
    }
}
