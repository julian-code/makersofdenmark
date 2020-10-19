using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class AddressChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "Address",
                newName: "PostCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "Address",
                newName: "ZipCode");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
