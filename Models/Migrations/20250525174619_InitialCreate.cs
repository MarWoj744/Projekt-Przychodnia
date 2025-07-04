﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Cennik = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Specjalizacja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badania", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osoby",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Haslo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rola = table.Column<int>(type: "int", nullable: false),
                   // PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tytul = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Specjalizacja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PESEL = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoby", x => x.Id);
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
                        name: "FK_Wizyty_Osoby_LekarzId",
                        column: x => x.LekarzId,
                        principalTable: "Osoby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wizyty_Osoby_PacjentId",
                        column: x => x.PacjentId,
                        principalTable: "Osoby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wizyty_Osoby_RecepcjonistkaId",
                        column: x => x.RecepcjonistkaId,
                        principalTable: "Osoby",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WykonaneBadania",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Wyniki = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WykonaneBadania_Wizyty_WizytaId",
                        column: x => x.WizytaId,
                        principalTable: "Wizyty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osoby_PESEL",
                table: "Osoby",
                column: "PESEL",
                unique: true,
                filter: "[PESEL] IS NOT NULL");

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
                name: "Osoby");
        }
    }
}
