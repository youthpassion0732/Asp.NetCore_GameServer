using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddedQuizCategoryIdInQuizHistoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizCategoryId",
                table: "QuizHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuizHistory_QuizCategoryId",
                table: "QuizHistory",
                column: "QuizCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizHistory_QuizCategory_QuizCategoryId",
                table: "QuizHistory",
                column: "QuizCategoryId",
                principalTable: "QuizCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizHistory_QuizCategory_QuizCategoryId",
                table: "QuizHistory");

            migrationBuilder.DropIndex(
                name: "IX_QuizHistory_QuizCategoryId",
                table: "QuizHistory");

            migrationBuilder.DropColumn(
                name: "QuizCategoryId",
                table: "QuizHistory");
        }
    }
}
