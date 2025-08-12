using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Account.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class DbCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UUID", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    SurName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    DialCode = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "public");
        }
    }
}
