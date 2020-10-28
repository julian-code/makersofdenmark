using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class MODUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MODUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MODRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MakerSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MODRole_MakerSpace_MakerSpaceId",
                        column: x => x.MakerSpaceId,
                        principalTable: "MakerSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MODRole_MODUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MODUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MODUserFollowMakerSpace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MakerSpaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODUserFollowMakerSpace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MODUserFollowMakerSpace_MakerSpace_MakerSpaceId",
                        column: x => x.MakerSpaceId,
                        principalTable: "MakerSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MODUserFollowMakerSpace_MODUser_UserId",
                        column: x => x.UserId,
                        principalTable: "MODUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MODRole_MakerSpaceId",
                table: "MODRole",
                column: "MakerSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MODRole_UserId",
                table: "MODRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MODUserFollowMakerSpace_MakerSpaceId",
                table: "MODUserFollowMakerSpace",
                column: "MakerSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MODUserFollowMakerSpace_UserId",
                table: "MODUserFollowMakerSpace",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MODRole");

            migrationBuilder.DropTable(
                name: "MODUserFollowMakerSpace");

            migrationBuilder.DropTable(
                name: "MODUser");
        }
    }
}
