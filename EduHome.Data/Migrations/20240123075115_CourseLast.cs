using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.Data.Migrations
{
    public partial class CourseLast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagBlog_Courses_CourseId",
                table: "TagBlog");

            migrationBuilder.DropIndex(
                name: "IX_TagBlog_CourseId",
                table: "TagBlog");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "TagBlog");

            migrationBuilder.CreateTable(
                name: "TagCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagCourse_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagCourse_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagCourse_CourseId",
                table: "TagCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TagCourse_TagId",
                table: "TagCourse",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagCourse");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "TagBlog",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TagBlog_CourseId",
                table: "TagBlog",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TagBlog_Courses_CourseId",
                table: "TagBlog",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
