using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class Veiculos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_usuario",
                table: "veiculo",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "veiculo");
        }
    }
}
