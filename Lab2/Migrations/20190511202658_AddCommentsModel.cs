using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class AddCommentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Expensess_ExpensesId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ExpensesId",
                table: "Comments",
                newName: "IX_Comments_ExpensesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Expensess_ExpensesId",
                table: "Comments",
                column: "ExpensesId",
                principalTable: "Expensess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Expensess_ExpensesId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ExpensesId",
                table: "Comment",
                newName: "IX_Comment_ExpensesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Expensess_ExpensesId",
                table: "Comment",
                column: "ExpensesId",
                principalTable: "Expensess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
