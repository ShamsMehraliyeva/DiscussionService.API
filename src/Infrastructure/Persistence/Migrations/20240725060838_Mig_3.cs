using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByIp",
                table: "RefreshTokens");

            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "RefreshTokens",
                newName: "ExpireDate");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "RefreshTokens",
                newName: "CreateDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ExpireDate",
                table: "RefreshTokens",
                newName: "Expires");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "RefreshTokens",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByIp",
                table: "RefreshTokens",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
