using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    /// <inheritdoc />
    public partial class changeQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupTitle",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Questions",
                newName: "TypeOfQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Value",
                table: "QuestionOption",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "QuestionOption");

            migrationBuilder.RenameColumn(
                name: "TypeOfQuestion",
                table: "Questions",
                newName: "GroupId");

            migrationBuilder.AddColumn<string>(
                name: "GroupTitle",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
