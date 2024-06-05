using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class userAjustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "User",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "situacao",
                table: "User",
                newName: "Situacao");

            migrationBuilder.RenameColumn(
                name: "senhaSalt",
                table: "User",
                newName: "SenhaSalt");

            migrationBuilder.RenameColumn(
                name: "senhaHash",
                table: "User",
                newName: "SenhaHash");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id_user",
                table: "User",
                newName: "Id_user");

            migrationBuilder.AddColumn<byte[]>(
                name: "CpfHash",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "CpfSalt",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfHash",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CpfSalt",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "User",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "Situacao",
                table: "User",
                newName: "situacao");

            migrationBuilder.RenameColumn(
                name: "SenhaSalt",
                table: "User",
                newName: "senhaSalt");

            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "User",
                newName: "senhaHash");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id_user",
                table: "User",
                newName: "id_user");
        }
    }
}
