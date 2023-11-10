using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab15.Data.Migrations
{
    public partial class lab1510111000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descrdescription",
                table: "Manufacturers",
                newName: "Country");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Manufacturers",
                newName: "Descrdescription");
        }
    }
}
