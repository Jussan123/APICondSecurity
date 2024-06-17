using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "veiculo_terceiro_id_usuario_fkey",
                table: "veiculo_terceiro");

            migrationBuilder.AlterColumn<int>(
                name: "id_veiculo",
                table: "veiculo_terceiro",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id_usuario",
                table: "veiculo_terceiro",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoUsuario",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "numero",
                table: "RFID",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "veiculo_terceiro_id_usuario_fkey",
                table: "veiculo_terceiro",
                column: "id_usuario",
                principalTable: "usuario",
                principalColumn: "id_usuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "veiculo_terceiro_id_usuario_fkey",
                table: "veiculo_terceiro");

            migrationBuilder.DropColumn(
                name: "IdTipoUsuario",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "id_veiculo",
                table: "veiculo_terceiro",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "id_usuario",
                table: "veiculo_terceiro",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "numero",
                table: "RFID",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "veiculo_terceiro_id_usuario_fkey",
                table: "veiculo_terceiro",
                column: "id_usuario",
                principalTable: "usuario",
                principalColumn: "id_usuario");
        }
    }
}
