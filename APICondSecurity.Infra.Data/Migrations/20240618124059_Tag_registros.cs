using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICondSecurity.Migrations
{
    /// <inheritdoc />
    public partial class Tag_registros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "tag",
                table: "registros",
                type: "character varying(18)",
                maxLength: 18,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tag",
                table: "registros");
        }
    }
}
