using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypto.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenCode = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenCode);
                });

            migrationBuilder.CreateTable(
                name: "TokensExchangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ExchangeType = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    TokenCode = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensExchangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokensExchangeHistory_Tokens_TokenCode",
                        column: x => x.TokenCode,
                        principalTable: "Tokens",
                        principalColumn: "TokenCode");
                });

            migrationBuilder.CreateTable(
                name: "TokensValueHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    TokenCode = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensValueHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokensValueHistory_Tokens_TokenCode",
                        column: x => x.TokenCode,
                        principalTable: "Tokens",
                        principalColumn: "TokenCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokensExchangeHistory_TokenCode",
                table: "TokensExchangeHistory",
                column: "TokenCode");

            migrationBuilder.CreateIndex(
                name: "IX_TokensValueHistory_TokenCode",
                table: "TokensValueHistory",
                column: "TokenCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokensExchangeHistory");

            migrationBuilder.DropTable(
                name: "TokensValueHistory");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
