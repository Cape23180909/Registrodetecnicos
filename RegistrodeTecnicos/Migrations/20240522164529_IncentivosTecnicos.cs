using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistrodeTecnicos.Migrations
{
    /// <inheritdoc />
    public partial class IncentivosTecnicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IncentivoId",
                table: "Tecnicos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncentivoId",
                table: "Tecnicos");
        }
    }
}
