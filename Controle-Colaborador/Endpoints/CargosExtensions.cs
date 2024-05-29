using ControleColaborador.API.Requests;
using ControleColaborador.API.Response;
using ControleColaborador.Shared.Dados.Dados;
using ControleColaborador.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace ControleColaborador.API.Endpoints;

public static class CargosExtensions
{
    public static void AddEndPointsCargos(this WebApplication app)
    {
        var grupoBuilder = app.MapGroup("cargos")
            .WithTags("Cargos");

        #region Endpoint Cargos
        /// <summary>
        /// Endpoint para obter uma lista de cargos.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos cargos.</param>
        /// <returns>Retorna uma lista de cargos.</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL e retorna a lista de cargos disponíveis.</remarks>
        grupoBuilder.MapGet("", ([FromServices] DAL<Cargo> dal) =>
        {
            return EntityListToResponseList(dal.Listar());
        });

        /// <summary>
        /// Endpoint para adicionar um novo cargo.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos cargos.</param>
        /// <param name="cargoRequest">Dados do cargo a ser criado.</param>
        /// <returns>Retorna 200 (OK) se o cargo for adicionado com sucesso.</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL. Ele converte os dados da requisição em uma entidade de cargo e a adiciona ao banco de dados.</remarks>
        grupoBuilder.MapPost("", ([FromServices] DAL<Cargo> dal, [FromBody] CargoRequest cargoRequest) =>
        {
            dal.Adicionar(RequestToEntity(cargoRequest));
        });

        /// <summary>
        /// Endpoint para obter um cargo pelo nome.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos cargos.</param>
        /// <param name="nome">Nome do cargo a ser buscado.</param>
        /// <returns>Retorna os dados do cargo se encontrado. Se o cargo não for encontrado, retorna 404 (Not Found).</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL. Ele busca um cargo pelo nome fornecido, realizando uma busca case-insensitive.</remarks>
        grupoBuilder.MapGet("{nome}", ([FromServices] DAL<Cargo> dal, string nome) =>
        {
            var cargo = dal.RecuperarPor(c => c.Nome.ToUpper().Equals(nome.ToUpper()));
            if (cargo is not null)
            {
                var response = EntityToResponse(cargo!);
                return Results.Ok(response);
            }
            return Results.NotFound("Cargo não encontrado.");
        });

        /// <summary>
        /// Endpoint para excluir um cargo pelo ID.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos cargos.</param>
        /// <param name="id">ID do cargo a ser excluído.</param>
        /// <returns>Retorna 204 (No Content) se o cargo for excluído com sucesso. Se o cargo não for encontrado, retorna 404 (Not Found).</returns>
        /// <remarks>Este método utiliza injeção de dependência para obter a instância de DAL e exclui o cargo correspondente ao ID fornecido.</remarks>
        grupoBuilder.MapDelete("{id}", ([FromServices] DAL<Cargo> dal, int id) =>
        {
            var cargo = dal.RecuperarPor(c => c.Id == id);
            if (cargo is null)
            {
                return Results.NotFound("Cargo para exclusão não encontrado.");
            }
            dal.Deletar(cargo);
            return Results.NoContent();
        });
    }
    #endregion

    private static Cargo RequestToEntity(CargoRequest cargoRequest)
    {
        return new Cargo() { Nome = cargoRequest.Nome };
    }

    private static ICollection<CargoResponse> EntityListToResponseList(IEnumerable<Cargo> cargos)
    {
        return cargos.Select(c => EntityToResponse(c)).ToList();
    }

    private static CargoResponse EntityToResponse(Cargo cargo)
    {
        return new CargoResponse(cargo.Id, cargo.Nome!);
    }
}
