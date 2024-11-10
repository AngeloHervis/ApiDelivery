using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ingrediente",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tipo_item = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(250)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidade_medida = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor_pago = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    valor_venda = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    marca = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<ulong>(type: "bit", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingrediente", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "item_extra",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    tipo_item = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(250)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidade_medida = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor_pago = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    valor_venda = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    marca = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<ulong>(type: "bit", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_extra", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    custo_variavel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    impostos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxa_cartao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(250)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidade_medida = table.Column<string>(type: "varchar(250)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor_pago = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    valor_venda = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    marca = table.Column<string>(type: "varchar(250)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    ativo = table.Column<ulong>(type: "bit", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produto_ifood",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    nome = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unidade_medida = table.Column<int>(type: "int", nullable: false),
                    valor_pago = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    marca = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    data_cadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    taxa_plano = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxa_transacao = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    taxa_repasse = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto_ifood", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produto_composicao",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ItemId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ItemExtraId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    unidade_medida = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tipo_item = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto_composicao", x => new { x.ProdutoId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_produto_composicao_ingrediente_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ingrediente",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_produto_composicao_item_extra_ItemExtraId",
                        column: x => x.ItemExtraId,
                        principalTable: "item_extra",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_produto_composicao_produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "produto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_produto_composicao_ItemExtraId",
                table: "produto_composicao",
                column: "ItemExtraId");

            migrationBuilder.CreateIndex(
                name: "IX_produto_composicao_ItemId",
                table: "produto_composicao",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produto_composicao");

            migrationBuilder.DropTable(
                name: "produto_ifood");

            migrationBuilder.DropTable(
                name: "ingrediente");

            migrationBuilder.DropTable(
                name: "item_extra");

            migrationBuilder.DropTable(
                name: "produto");
        }
    }
}
