using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Lekarze_LekarzId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Pacjenci_PacjentId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Recepcjonistki_RecepcjonistkaId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_WykonaneBadania_Badania_BadanieId",
                table: "WykonaneBadania");

            migrationBuilder.DropTable(
                name: "Lekarze");

            migrationBuilder.DropTable(
                name: "Pacjenci");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recepcjonistki",
                table: "Recepcjonistki");

            migrationBuilder.DropColumn(
                name: "Cena",
                table: "WykonaneBadania");

            migrationBuilder.RenameTable(
                name: "Recepcjonistki",
                newName: "Osoby");

            migrationBuilder.AddColumn<string>(
                name: "Specjalizacja",
                table: "Badania",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PESEL",
                table: "Osoby",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Osoby",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Haslo",
                table: "Osoby",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "Rola",
                table: "Osoby",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Specjalizacja",
                table: "Osoby",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tytul",
                table: "Osoby",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Osoby",
                table: "Osoby",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Osoby_PESEL",
                table: "Osoby",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Osoby_LekarzId",
                table: "Wizyty",
                column: "LekarzId",
                principalTable: "Osoby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Osoby_PacjentId",
                table: "Wizyty",
                column: "PacjentId",
                principalTable: "Osoby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Osoby_RecepcjonistkaId",
                table: "Wizyty",
                column: "RecepcjonistkaId",
                principalTable: "Osoby",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WykonaneBadania_Badania_BadanieId",
                table: "WykonaneBadania",
                column: "BadanieId",
                principalTable: "Badania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Osoby_LekarzId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Osoby_PacjentId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_Wizyty_Osoby_RecepcjonistkaId",
                table: "Wizyty");

            migrationBuilder.DropForeignKey(
                name: "FK_WykonaneBadania_Badania_BadanieId",
                table: "WykonaneBadania");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Osoby",
                table: "Osoby");

            migrationBuilder.DropIndex(
                name: "IX_Osoby_PESEL",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "Specjalizacja",
                table: "Badania");

            migrationBuilder.DropColumn(
                name: "Rola",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "Specjalizacja",
                table: "Osoby");

            migrationBuilder.DropColumn(
                name: "Tytul",
                table: "Osoby");

            migrationBuilder.RenameTable(
                name: "Osoby",
                newName: "Recepcjonistki");

            migrationBuilder.AddColumn<decimal>(
                name: "Cena",
                table: "WykonaneBadania",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "PESEL",
                table: "Recepcjonistki",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Recepcjonistki",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Haslo",
                table: "Recepcjonistki",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recepcjonistki",
                table: "Recepcjonistki",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Lekarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekarze", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacjenci",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacjenci", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Lekarze_LekarzId",
                table: "Wizyty",
                column: "LekarzId",
                principalTable: "Lekarze",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Pacjenci_PacjentId",
                table: "Wizyty",
                column: "PacjentId",
                principalTable: "Pacjenci",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wizyty_Recepcjonistki_RecepcjonistkaId",
                table: "Wizyty",
                column: "RecepcjonistkaId",
                principalTable: "Recepcjonistki",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WykonaneBadania_Badania_BadanieId",
                table: "WykonaneBadania",
                column: "BadanieId",
                principalTable: "Badania",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
