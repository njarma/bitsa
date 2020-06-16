using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace Bitsa.Base.Migrations
{
    public partial class recreateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    First_Name = table.Column<string>(maxLength: 50, nullable: true),
                    Last_Name = table.Column<string>(maxLength: 50, nullable: true),
                    Alias = table.Column<string>(maxLength: 10, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Entry_Date = table.Column<DateTime>(nullable: false, defaultValueSql: "Now()"),
                    Enabled = table.Column<short>(nullable: false, defaultValue: (short)1),
                    Balance = table.Column<float>(nullable: false, defaultValue: 0f),
                    Administrator = table.Column<short>(nullable: false, defaultValue: (short)0)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
