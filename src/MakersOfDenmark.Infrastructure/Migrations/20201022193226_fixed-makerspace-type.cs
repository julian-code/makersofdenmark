using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class fixedmakerspacetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpace_MakerSpaceType_MakerSpaceTypeId",
                table: "MakerSpace");

            migrationBuilder.DropTable(
                name: "MakerSpaceType");

            migrationBuilder.DropIndex(
                name: "IX_MakerSpace_MakerSpaceTypeId",
                table: "MakerSpace");

            migrationBuilder.DropColumn(
                name: "MakerSpaceTypeId",
                table: "MakerSpace");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MakerSpaceTypeId",
                table: "MakerSpace",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MakerSpaceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakerSpaceType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MakerSpace_MakerSpaceTypeId",
                table: "MakerSpace",
                column: "MakerSpaceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpace_MakerSpaceType_MakerSpaceTypeId",
                table: "MakerSpace",
                column: "MakerSpaceTypeId",
                principalTable: "MakerSpaceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
