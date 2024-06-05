CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Tokens" (
    "TokenCode" VARCHAR NOT NULL CONSTRAINT "PK_Tokens" PRIMARY KEY
);

CREATE TABLE "TokensExchangeHistory" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TokensExchangeHistory" PRIMARY KEY AUTOINCREMENT,
    "RecordedDate" DATETIME NOT NULL,
    "ExchangeType" INTEGER NOT NULL,
    "Value" REAL NOT NULL,
    "TokenCode" VARCHAR NULL,
    CONSTRAINT "FK_TokensExchangeHistory_Tokens_TokenCode" FOREIGN KEY ("TokenCode") REFERENCES "Tokens" ("TokenCode")
);

CREATE TABLE "TokensValueHistory" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_TokensValueHistory" PRIMARY KEY AUTOINCREMENT,
    "RecordedDate" DATETIME NOT NULL,
    "Value" REAL NOT NULL,
    "TokenCode" VARCHAR NULL,
    CONSTRAINT "FK_TokensValueHistory_Tokens_TokenCode" FOREIGN KEY ("TokenCode") REFERENCES "Tokens" ("TokenCode")
);

CREATE INDEX "IX_TokensExchangeHistory_TokenCode" ON "TokensExchangeHistory" ("TokenCode");

CREATE INDEX "IX_TokensValueHistory_TokenCode" ON "TokensValueHistory" ("TokenCode");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240605110043_InitialCreate', '8.0.6');

COMMIT;