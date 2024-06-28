using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class CpfUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "termoAceite",
                table: "User",
                newName: "TermoAceite");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "TermoAceite",
                table: "User",
                newName: "termoAceite");
        }
    }
}
