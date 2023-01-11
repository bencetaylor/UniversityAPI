using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDatabase.Migrations
{
    public partial class AddNewFieldToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Timetable",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Ismeretlen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timetable",
                table: "Course");
        }
    }
}
