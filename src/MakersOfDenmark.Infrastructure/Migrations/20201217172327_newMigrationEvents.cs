using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class newMigrationEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_MakerSpace_MakerSpaceId",
                table: "Event");

            migrationBuilder.AlterColumn<Guid>(
                name: "MakerSpaceId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_MakerSpace_MakerSpaceId",
                table: "Event",
                column: "MakerSpaceId",
                principalTable: "MakerSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_MakerSpace_MakerSpaceId",
                table: "Event");

            migrationBuilder.AlterColumn<Guid>(
                name: "MakerSpaceId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_MakerSpace_MakerSpaceId",
                table: "Event",
                column: "MakerSpaceId",
                principalTable: "MakerSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
