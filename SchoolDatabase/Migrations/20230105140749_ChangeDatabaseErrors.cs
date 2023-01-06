using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolDatabase.Migrations
{
    public partial class ChangeDatabaseErrors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostitionId",
                table: "Teacher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostitionId",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
