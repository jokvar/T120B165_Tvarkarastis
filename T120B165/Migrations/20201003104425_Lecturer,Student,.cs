using Microsoft.EntityFrameworkCore.Migrations;

namespace T120B165.Migrations
{
    public partial class LecturerStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Lecturers_LecturerId",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "Modules",
                newName: "LecturerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Modules",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_LecturerId",
                table: "Modules",
                newName: "IX_Modules_LecturerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lecturers",
                newName: "ID");

            migrationBuilder.AddColumn<int>(
                name: "ModuleID",
                table: "Students",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LecturerID",
                table: "Modules",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ModuleID",
                table: "Students",
                column: "ModuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Lecturers_LecturerID",
                table: "Modules",
                column: "LecturerID",
                principalTable: "Lecturers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Modules_ModuleID",
                table: "Students",
                column: "ModuleID",
                principalTable: "Modules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Lecturers_LecturerID",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Modules_ModuleID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ModuleID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ModuleID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LecturerID",
                table: "Modules",
                newName: "LecturerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Modules",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_LecturerID",
                table: "Modules",
                newName: "IX_Modules_LecturerId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Lecturers",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Modules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Lecturers_LecturerId",
                table: "Modules",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
