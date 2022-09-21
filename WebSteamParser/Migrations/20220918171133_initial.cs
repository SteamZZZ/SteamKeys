using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSteamParser.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "GAME_LIST",
                schema: "dbo",
                columns: table => new
                {
                    GL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GL_NAME = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GL_PRICE = table.Column<double>(type: "float", nullable: false),
                    GL_AVAILABILITY = table.Column<bool>(type: "bit", nullable: true),
                    GL_PRICE_RU = table.Column<double>(type: "float", nullable: true),
                    GL_PRICE_KZ = table.Column<double>(type: "float", nullable: true),
                    GL_PRICE_TR = table.Column<double>(type: "float", nullable: true),
                    GL_STEAM_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GAME_LIS__E2DBDB9144DC3972", x => x.GL_ID);
                });

            migrationBuilder.CreateTable(
                name: "GAME_LIST_TEMP_KZ",
                schema: "dbo",
                columns: table => new
                {
                    GLT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GLT_PRICE = table.Column<double>(type: "float", nullable: true),
                    GLT_STEAM_ID = table.Column<int>(type: "int", nullable: true),
                    GLT_IMAGE_PATH = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GAME_LIST_TEMP_RU",
                schema: "dbo",
                columns: table => new
                {
                    GLT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GLT_PRICE = table.Column<double>(type: "float", nullable: true),
                    GLT_STEAM_ID = table.Column<int>(type: "int", nullable: true),
                    GLT_IMAGE_PATH = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GAME_LIST_TEMP_TR",
                schema: "dbo",
                columns: table => new
                {
                    GLT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GLT_PRICE = table.Column<double>(type: "float", nullable: true),
                    GLT_STEAM_ID = table.Column<int>(type: "int", nullable: true),
                    GLT_IMAGE_PATH = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "GAME_LIST_TEMP_USA",
                schema: "dbo",
                columns: table => new
                {
                    GLT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GLT_PRICE = table.Column<double>(type: "float", nullable: true),
                    GLT_STEAM_ID = table.Column<int>(type: "int", nullable: true),
                    GLT_IMAGE_PATH = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__GAME_LIS__EA70666574FF3FCD", x => x.GLT_ID);
                });

            migrationBuilder.CreateTable(
                name: "OTHER_SITE_LIST",
                schema: "dbo",
                columns: table => new
                {
                    OSL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OSL_SITE_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    OSL_NAME = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    OSL_PRICE = table.Column<double>(type: "float", nullable: true),
                    OSL_REF = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    OSL_STEAM_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OTHER_SI__A46D1E121331F343", x => x.OSL_ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_FAVORITES",
                schema: "dbo",
                columns: table => new
                {
                    UF_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UF_UL_ID = table.Column<int>(type: "int", nullable: true),
                    UF_GL_STEAM_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER_FAV__8FD45A596343B8CC", x => x.UF_ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_LIST",
                schema: "dbo",
                columns: table => new
                {
                    UL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UL_LOGIN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UL_PASSWORD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UL_LAST_VISIT = table.Column<DateTime>(type: "datetime", nullable: true),
                    UL_BALANCE = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER_LIS__FB8CA7AEF298C961", x => x.UL_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GAME_LIST",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GAME_LIST_TEMP_KZ",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GAME_LIST_TEMP_RU",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GAME_LIST_TEMP_TR",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "GAME_LIST_TEMP_USA",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OTHER_SITE_LIST",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "USER_FAVORITES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "USER_LIST",
                schema: "dbo");
        }
    }
}
