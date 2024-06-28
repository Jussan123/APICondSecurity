using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class Termo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "termoAceite",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "termoAceite",
                table: "User");
        }
    }
}
