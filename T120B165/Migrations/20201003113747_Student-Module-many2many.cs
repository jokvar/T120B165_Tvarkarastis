using Microsoft.EntityFrameworkCore.Migrations;

namespace T120B165.Migrations
{
    public partial class StudentModulemany2many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Modules_ModuleID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ModuleID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ModuleID",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "ModuleStudentMapping",
                columns: table => new
                {
                    ModuleID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStudentMapping", x => new { x.ModuleID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_ModuleStudentMapping_Modules_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Modules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ModuleStudentMapping_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStudentMapping_StudentID",
                table: "ModuleStudentMapping",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleStudentMapping");

            migrationBuilder.AddColumn<int>(
                name: "ModuleID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ModuleID",
                table: "Students",
                column: "ModuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Modules_ModuleID",
                table: "Students",
                column: "ModuleID",
                principalTable: "Modules",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
