﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class makerspace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpace_Organization_OrganizationId",
                table: "MakerSpace");

            migrationBuilder.DropTable(
                name: "CategoryTool");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_MakerSpace_OrganizationId",
                table: "MakerSpace");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "MakerSpace");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "MakerSpace",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organization",
                table: "MakerSpace");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "MakerSpace",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organization_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTool",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTool", x => new { x.CategoriesId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_CategoryTool_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTool_Tool_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tool",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MakerSpace_OrganizationId",
                table: "MakerSpace",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTool_ToolsId",
                table: "CategoryTool",
                column: "ToolsId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_AddressId",
                table: "Organization",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpace_Organization_OrganizationId",
                table: "MakerSpace",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
