using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desafio_Online_Applications.API.Migrations
{
    /// <inheritdoc />
    public partial class BancoDeDados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Erros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Valor = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    Cartao = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    DonoLoja = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    NomeLoja = table.Column<string>(type: "nvarchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    Cartao = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    DonoLoja = table.Column<string>(type: "nvarchar(14)", nullable: false),
                    NomeLoja = table.Column<string>(type: "nvarchar(18)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Erros");

            migrationBuilder.DropTable(
                name: "Operacoes");
        }
    }
}
