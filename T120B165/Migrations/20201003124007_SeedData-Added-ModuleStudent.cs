using Microsoft.EntityFrameworkCore.Migrations;

namespace T120B165.Migrations
{
    public partial class SeedDataAddedModuleStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudent_Students_ModuleID",
                table: "ModuleStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudent_Modules_StudentID",
                table: "ModuleStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleStudent",
                table: "ModuleStudent");

            migrationBuilder.RenameTable(
                name: "ModuleStudent",
                newName: "ModuleStudents");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStudent_StudentID",
                table: "ModuleStudents",
                newName: "IX_ModuleStudents_StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleStudents",
                table: "ModuleStudents",
                columns: new[] { "ModuleID", "StudentID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudents_Students_ModuleID",
                table: "ModuleStudents",
                column: "ModuleID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudents_Modules_StudentID",
                table: "ModuleStudents",
                column: "StudentID",
                principalTable: "Modules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudents_Students_ModuleID",
                table: "ModuleStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_ModuleStudents_Modules_StudentID",
                table: "ModuleStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleStudents",
                table: "ModuleStudents");

            migrationBuilder.RenameTable(
                name: "ModuleStudents",
                newName: "ModuleStudent");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleStudents_StudentID",
                table: "ModuleStudent",
                newName: "IX_ModuleStudent_StudentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleStudent",
                table: "ModuleStudent",
                columns: new[] { "ModuleID", "StudentID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudent_Students_ModuleID",
                table: "ModuleStudent",
                column: "ModuleID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleStudent_Modules_StudentID",
                table: "ModuleStudent",
                column: "StudentID",
                principalTable: "Modules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
