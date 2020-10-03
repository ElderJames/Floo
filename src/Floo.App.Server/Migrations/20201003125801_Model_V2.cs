using Microsoft.EntityFrameworkCore.Migrations;

namespace Floo.App.Server.Migrations
{
    public partial class Model_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Contents_ContnetId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ContentId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "ContnetId",
                table: "Articles",
                newName: "ContentId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ContnetId",
                table: "Articles",
                newName: "IX_Articles_ContentId");

            migrationBuilder.AlterColumn<long>(
                name: "QuestionId",
                table: "Answers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ContentId",
                table: "Answers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ContentId",
                table: "Answers",
                column: "ContentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Contents_ContentId",
                table: "Articles",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Contents_ContentId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Answers_ContentId",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "ContentId",
                table: "Articles",
                newName: "ContnetId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_ContentId",
                table: "Articles",
                newName: "IX_Articles_ContnetId");

            migrationBuilder.AlterColumn<long>(
                name: "QuestionId",
                table: "Answers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "ContentId",
                table: "Answers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ContentId",
                table: "Answers",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Contents_ContentId",
                table: "Answers",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Contents_ContnetId",
                table: "Articles",
                column: "ContnetId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
