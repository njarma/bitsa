using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Bitsa.Base.Migrations
{
    public partial class removeUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Administrator = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    Alias = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Balance = table.Column<float>(type: "float", nullable: false, defaultValue: 0f),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Entry_Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "Now()"),
                    First_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Last_Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }
    }
}
