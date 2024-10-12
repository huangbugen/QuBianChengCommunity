using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileDownLoadSystem.Core.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "FilePackages",
                newName: "FilesId");

            migrationBuilder.CreateIndex(
                name: "IX_FilePackages_FilesId",
                table: "FilePackages",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages");

            migrationBuilder.DropIndex(
                name: "IX_FilePackages_FilesId",
                table: "FilePackages");

            migrationBuilder.RenameColumn(
                name: "FilesId",
                table: "FilePackages",
                newName: "FileId");
        }
    }
}
