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
                    TokenCode = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: false),
                    PurchaseValue = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenCode);
                });

            migrationBuilder.CreateTable(
                name: "TokensHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TokenCode = table.Column<string>(type: "VARCHAR", maxLength: 25, nullable: false),
                    RecordedDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false),
                    TokenCode1 = table.Column<string>(type: "VARCHAR", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TokensHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TokensHistory_Tokens_TokenCode1",
                        column: x => x.TokenCode1,
                        principalTable: "Tokens",
                        principalColumn: "TokenCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TokensHistory_TokenCode1",
                table: "TokensHistory",
                column: "TokenCode1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TokensHistory");

            migrationBuilder.DropTable(
                name: "Tokens");
        }
    }
}
