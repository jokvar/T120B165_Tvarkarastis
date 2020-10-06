using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace T120B165.Migrations
{
    public partial class fixedstudenttimetablerelationshipinitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformalGatherings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformalGatherings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lecturers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 60, nullable: false),
                    ApiKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lecturers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Username = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 60, nullable: false),
                    ApiKey = table.Column<string>(nullable: true),
                    Vidko = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    LecturerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Modules_Lecturers_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "Lecturers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InformalGatheringStudents",
                columns: table => new
                {
                    InformalGatheringID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformalGatheringStudents", x => new { x.InformalGatheringID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_InformalGatheringStudents_Students_InformalGatheringID",
                        column: x => x.InformalGatheringID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InformalGatheringStudents_InformalGatherings_StudentID",
                        column: x => x.StudentID,
                        principalTable: "InformalGatherings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeTables_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lectures",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    Hall = table.Column<string>(nullable: true),
                    ModuleID = table.Column<int>(nullable: false),
                    LecturerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lectures_Lecturers_LecturerID",
                        column: x => x.LecturerID,
                        principalTable: "Lecturers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lectures_Modules_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Modules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModuleStudents",
                columns: table => new
                {
                    ModuleID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleStudents", x => new { x.ModuleID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_ModuleStudents_Students_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ModuleStudents_Modules_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Modules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentTimeTables",
                columns: table => new
                {
                    InformalGatheringID = table.Column<int>(nullable: false),
                    TimeTableID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTimeTables", x => new { x.InformalGatheringID, x.TimeTableID });
                    table.ForeignKey(
                        name: "FK_StudentTimeTables_TimeTables_InformalGatheringID",
                        column: x => x.InformalGatheringID,
                        principalTable: "TimeTables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentTimeTables_InformalGatherings_TimeTableID",
                        column: x => x.TimeTableID,
                        principalTable: "InformalGatherings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureStudents",
                columns: table => new
                {
                    LectureID = table.Column<int>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureStudents", x => new { x.LectureID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_LectureStudents_Students_LectureID",
                        column: x => x.LectureID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureStudents_Lectures_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Lectures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LectureTimeTables",
                columns: table => new
                {
                    LectureID = table.Column<int>(nullable: false),
                    TimeTableID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureTimeTables", x => new { x.LectureID, x.TimeTableID });
                    table.ForeignKey(
                        name: "FK_LectureTimeTables_TimeTables_LectureID",
                        column: x => x.LectureID,
                        principalTable: "TimeTables",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LectureTimeTables_Lectures_TimeTableID",
                        column: x => x.TimeTableID,
                        principalTable: "Lectures",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InformalGatheringStudents_StudentID",
                table: "InformalGatheringStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_LecturerID",
                table: "Lectures",
                column: "LecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ModuleID",
                table: "Lectures",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_LectureStudents_StudentID",
                table: "LectureStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_LectureTimeTables_TimeTableID",
                table: "LectureTimeTables",
                column: "TimeTableID");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_LecturerID",
                table: "Modules",
                column: "LecturerID");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleStudents_StudentID",
                table: "ModuleStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTimeTables_TimeTableID",
                table: "StudentTimeTables",
                column: "TimeTableID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_StudentID",
                table: "TimeTables",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformalGatheringStudents");

            migrationBuilder.DropTable(
                name: "LectureStudents");

            migrationBuilder.DropTable(
                name: "LectureTimeTables");

            migrationBuilder.DropTable(
                name: "ModuleStudents");

            migrationBuilder.DropTable(
                name: "StudentTimeTables");

            migrationBuilder.DropTable(
                name: "Lectures");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "InformalGatherings");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Lecturers");
        }
    }
}
