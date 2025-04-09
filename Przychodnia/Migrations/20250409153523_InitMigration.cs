using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Badania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cennik = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lekarze",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacjenci", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recepcjonistki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepcjonistki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wizyty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PacjentId = table.Column<int>(type: "int", nullable: false),
                    LekarzId = table.Column<int>(type: "int", nullable: false),
                    RecepcjonistkaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wizyty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wizyty_Lekarze_LekarzId",
                        column: x => x.LekarzId,
                        principalTable: "Lekarze",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wizyty_Pacjenci_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Pacjenci",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wizyty_Recepcjonistki_RecepcjonistkaId",
                        column: x => x.RecepcjonistkaId,
                        principalTable: "Recepcjonistki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WykonaneBadania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wyniki = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WizytaId = table.Column<int>(type: "int", nullable: false),
                    BadanieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WykonaneBadania", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WykonaneBadania_Badania_BadanieId",
                        column: x => x.BadanieId,
                        principalTable: "Badania",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WykonaneBadania_Wizyty_WizytaId",
                        column: x => x.WizytaId,
                        principalTable: "Wizyty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_LekarzId",
                table: "Wizyty",
                column: "LekarzId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_PacjentId",
                table: "Wizyty",
                column: "PacjentId");

            migrationBuilder.CreateIndex(
                name: "IX_Wizyty_RecepcjonistkaId",
                table: "Wizyty",
                column: "RecepcjonistkaId");

            migrationBuilder.CreateIndex(
                name: "IX_WykonaneBadania_BadanieId",
                table: "WykonaneBadania",
                column: "BadanieId");

            migrationBuilder.CreateIndex(
                name: "IX_WykonaneBadania_WizytaId",
                table: "WykonaneBadania",
                column: "WizytaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WykonaneBadania");

            migrationBuilder.DropTable(
                name: "Badania");

            migrationBuilder.DropTable(
                name: "Wizyty");

            migrationBuilder.DropTable(
                name: "Lekarze");

            migrationBuilder.DropTable(
                name: "Pacjenci");

            migrationBuilder.DropTable(
                name: "Recepcjonistki");
        }
    }
}
