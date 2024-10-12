using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileDownLoadSystem.Core.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages");

            migrationBuilder.AlterColumn<int>(
                name: "FilesId",
                table: "FilePackages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FilePackages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FilePackages");

            migrationBuilder.AlterColumn<int>(
                name: "FilesId",
                table: "FilePackages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilePackages_Files_FilesId",
                table: "FilePackages",
                column: "FilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
