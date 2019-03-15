using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdatedQuizCategoryIdInQuestionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Game_GameId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Question",
                newName: "QuizCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_GameId",
                table: "Question",
                newName: "IX_Question_QuizCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_QuizCategory_QuizCategoryId",
                table: "Question",
                column: "QuizCategoryId",
                principalTable: "QuizCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_QuizCategory_QuizCategoryId",
                table: "Question");

            migrationBuilder.RenameColumn(
                name: "QuizCategoryId",
                table: "Question",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Question_QuizCategoryId",
                table: "Question",
                newName: "IX_Question_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Game_GameId",
                table: "Question",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
