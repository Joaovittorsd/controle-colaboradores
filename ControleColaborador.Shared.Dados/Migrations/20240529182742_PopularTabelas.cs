using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleColaborador.Shared.Dados.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Gerente de Vendas" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Analista de Marketing" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Desenvolvedor Full Stack" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Engenheiro de Software" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Designer UX/UI" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Analista Financeiro" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Gestor de Projetos" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Técnico de Suporte" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Engenheiro de Dados" });
            migrationBuilder.InsertData("Cargos", new string[] { "Nome" }, new object[] { "Especialista em Recursos Humanos" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Colaboradores");
        }
    }
}
