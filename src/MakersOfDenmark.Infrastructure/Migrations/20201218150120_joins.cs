using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class joins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Badges_MakerSpace_MakerSpaceId",
                table: "Badges");

            migrationBuilder.DropForeignKey(
                name: "FK_Badges_Users_UserId",
                table: "Badges");

            migrationBuilder.DropIndex(
                name: "IX_Badges_MakerSpaceId",
                table: "Badges");

            migrationBuilder.DropIndex(
                name: "IX_Badges_UserId",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "MakerSpaceId",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Badges");

            migrationBuilder.CreateTable(
                name: "BadgeAssignedToUser",
                columns: table => new
                {
                    BadgesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadgeAssignedToUser", x => new { x.BadgesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BadgeAssignedToUser_Badges_BadgesId",
                        column: x => x.BadgesId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BadgeAssignedToUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MakerSpaceHasBadges",
                columns: table => new
                {
                    BadgesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MakerSpacesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakerSpaceHasBadges", x => new { x.BadgesId, x.MakerSpacesId });
                    table.ForeignKey(
                        name: "FK_MakerSpaceHasBadges_Badges_BadgesId",
                        column: x => x.BadgesId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MakerSpaceHasBadges_MakerSpace_MakerSpacesId",
                        column: x => x.MakerSpacesId,
                        principalTable: "MakerSpace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BadgeAssignedToUser_UsersId",
                table: "BadgeAssignedToUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MakerSpaceHasBadges_MakerSpacesId",
                table: "MakerSpaceHasBadges",
                column: "MakerSpacesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadgeAssignedToUser");

            migrationBuilder.DropTable(
                name: "MakerSpaceHasBadges");

            migrationBuilder.AddColumn<Guid>(
                name: "MakerSpaceId",
                table: "Badges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Badges",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Badges_MakerSpaceId",
                table: "Badges",
                column: "MakerSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Badges_UserId",
                table: "Badges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_MakerSpace_MakerSpaceId",
                table: "Badges",
                column: "MakerSpaceId",
                principalTable: "MakerSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Badges_Users_UserId",
                table: "Badges",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
