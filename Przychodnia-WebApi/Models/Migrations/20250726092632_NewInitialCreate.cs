using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Osoby",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Osoby",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Osoby");
        }
    }
}
