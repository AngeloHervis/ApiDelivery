using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class VoltandoAtras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "produto",
                newName: "quantidade_porcao");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "item_extra",
                newName: "quantidade_estoque");

            migrationBuilder.RenameColumn(
                name: "quantidade",
                table: "ingrediente",
                newName: "quantidade_estoque");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "quantidade_porcao",
                table: "produto",
                newName: "quantidade");

            migrationBuilder.RenameColumn(
                name: "quantidade_estoque",
                table: "item_extra",
                newName: "quantidade");

            migrationBuilder.RenameColumn(
                name: "quantidade_estoque",
                table: "ingrediente",
                newName: "quantidade");
        }
    }
}
