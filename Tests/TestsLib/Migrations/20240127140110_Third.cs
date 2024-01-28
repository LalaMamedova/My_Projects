using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestsLib.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentQuestion",
                table: "Tests");

            migrationBuilder.AddColumn<bool>(
                name: "isAuth",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isAuth",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "CurrentQuestion",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
