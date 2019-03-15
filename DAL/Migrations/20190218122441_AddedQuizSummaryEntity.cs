using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedQuizSummaryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<string>(nullable: true),
                    IsPass = table.Column<bool>(nullable: false),
                    TotalQuestions = table.Column<int>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    ObtainedScore = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    QuizCategoryId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizSummary_QuizCategory_QuizCategoryId",
                        column: x => x.QuizCategoryId,
                        principalTable: "QuizCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizSummary_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizSummary_QuizCategoryId",
                table: "QuizSummary",
                column: "QuizCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizSummary_UserId",
                table: "QuizSummary",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizSummary");
        }
    }
}
