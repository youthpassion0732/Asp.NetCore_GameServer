using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdatedQuizHistoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizHistory_Answer_AnswerId",
                table: "QuizHistory");

            migrationBuilder.RenameColumn(
                name: "IsTrue",
                table: "QuizHistory",
                newName: "IsCorrectAnswer");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "QuizHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_QuizHistory_Answer_AnswerId",
                table: "QuizHistory",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizHistory_Answer_AnswerId",
                table: "QuizHistory");

            migrationBuilder.RenameColumn(
                name: "IsCorrectAnswer",
                table: "QuizHistory",
                newName: "IsTrue");

            migrationBuilder.AlterColumn<int>(
                name: "AnswerId",
                table: "QuizHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizHistory_Answer_AnswerId",
                table: "QuizHistory",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
