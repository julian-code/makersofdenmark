using Microsoft.EntityFrameworkCore.Migrations;

namespace MakersOfDenmark.Infrastructure.Migrations
{
    public partial class tool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpaceTool_MakerSpace_MakerSpacesId",
                table: "MakerSpaceTool");

            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpaceTool_Tool_ToolsId",
                table: "MakerSpaceTool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MakerSpaceTool",
                table: "MakerSpaceTool");

            migrationBuilder.RenameTable(
                name: "MakerSpaceTool",
                newName: "MakerSpaceHasTools");

            migrationBuilder.RenameIndex(
                name: "IX_MakerSpaceTool_ToolsId",
                table: "MakerSpaceHasTools",
                newName: "IX_MakerSpaceHasTools_ToolsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MakerSpaceHasTools",
                table: "MakerSpaceHasTools",
                columns: new[] { "MakerSpacesId", "ToolsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpaceHasTools_MakerSpace_MakerSpacesId",
                table: "MakerSpaceHasTools",
                column: "MakerSpacesId",
                principalTable: "MakerSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpaceHasTools_Tool_ToolsId",
                table: "MakerSpaceHasTools",
                column: "ToolsId",
                principalTable: "Tool",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpaceHasTools_MakerSpace_MakerSpacesId",
                table: "MakerSpaceHasTools");

            migrationBuilder.DropForeignKey(
                name: "FK_MakerSpaceHasTools_Tool_ToolsId",
                table: "MakerSpaceHasTools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MakerSpaceHasTools",
                table: "MakerSpaceHasTools");

            migrationBuilder.RenameTable(
                name: "MakerSpaceHasTools",
                newName: "MakerSpaceTool");

            migrationBuilder.RenameIndex(
                name: "IX_MakerSpaceHasTools_ToolsId",
                table: "MakerSpaceTool",
                newName: "IX_MakerSpaceTool_ToolsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MakerSpaceTool",
                table: "MakerSpaceTool",
                columns: new[] { "MakerSpacesId", "ToolsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpaceTool_MakerSpace_MakerSpacesId",
                table: "MakerSpaceTool",
                column: "MakerSpacesId",
                principalTable: "MakerSpace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MakerSpaceTool_Tool_ToolsId",
                table: "MakerSpaceTool",
                column: "ToolsId",
                principalTable: "Tool",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
