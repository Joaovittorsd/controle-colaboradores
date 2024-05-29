using ControleColaborador.API.Requests;
using ControleColaborador.API.Response;
using ControleColaborador.Shared.Dados.Dados;
using ControleColaborador.Shared.Modelos.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace ControleColaborador.API.Endpoints;

public static class ColaboradoresExtensions
{
    public static void AddEndPointsColaboradores(this WebApplication app)
    {
        var grupoBuilder = app.MapGroup("colaboradores")
            .WithTags("Colaboradores");

        #region Endpoint Colaboradores
        /// <summary>
        /// Endpoint para obter uma lista de colaboradores.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos colaboradores.</param>
        /// <returns>Retorna uma lista de colaboradores. Se não houver colaboradores, retorna 404 (Not Found).</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL.</remarks>
        grupoBuilder.MapGet("", ([FromServices] DAL<Colaborador> dal) =>
        {
            var listaDeColaboradores = dal.Listar();
            if (listaDeColaboradores is null)
            {
                return Results.NotFound();
            }
            var listaDeColaboradorResponse = EntityListToResponseList(listaDeColaboradores);
            return Results.Ok(listaDeColaboradorResponse);
        });

        /// <summary>
        /// Endpoint para obter um colaborador pelo nome.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos colaboradores.</param>
        /// <param name="nome">Nome do colaborador a ser buscado.</param>
        /// <returns>Retorna os dados do colaborador. Se o colaborador não for encontrado, retorna 404 (Not Found).</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL e faz uma busca case-insensitive pelo nome do colaborador.</remarks>
        grupoBuilder.MapGet("{nome}", ([FromServices] DAL<Colaborador> dal, string nome) =>
        {
            var colaborador = dal.RecuperarPor(c => c.Nome!.Trim().Equals(HttpUtility.UrlDecode(nome), StringComparison.OrdinalIgnoreCase));
            if (colaborador is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(colaborador));

        });

        /// <summary>
        /// Endpoint para criar um novo colaborador.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos colaboradores.</param>
        /// <param name="colaboradorRequest">Dados do colaborador a ser criado.</param>
        /// <returns>Retorna 200 (OK) se o colaborador for criado com sucesso.</returns>
        /// <remarks>Este método usa injeção de dependência para obter as instâncias de DAL. Ele cria um novo colaborador com base nos dados fornecidos e o adiciona ao banco de dados.</remarks>
        grupoBuilder.MapPost("", ([FromServices] DAL<Colaborador> dal, [FromBody] ColaboradorRequest colaboradorRequest) =>
        {
            var colaborador = new Colaborador(colaboradorRequest.Nome)
            {
                Nome = colaboradorRequest.Nome,
                Email = colaboradorRequest.Email,
                Telefone = colaboradorRequest.Telefone,
                CargoId = colaboradorRequest.CargoId
            };
            dal.Adicionar(colaborador);
            return Results.Ok();
        });

        /// <summary>
        /// Endpoint para excluir um colaborador pelo ID.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados dos colaboradores.</param>
        /// <param name="id">ID do colaborador a ser excluído.</param>
        /// <returns>Retorna 404 (Not Found) se o colaborador não for encontrado ou 204 (No Content) se o colaborador for excluído com sucesso.</returns>
        /// <remarks>Este método usa injeção de dependência para obter a instância de DAL. Ele busca o colaborador pelo ID fornecido e o exclui do banco de dados, se encontrado.</remarks>
        grupoBuilder.MapDelete("{id}", ([FromServices] DAL<Colaborador> dal, int id) =>
        {
            var colaborador = dal.RecuperarPor(c => c.Id == id);
            if (colaborador is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(colaborador);
            return Results.NoContent();
        });

        /// <summary>
        /// Endpoint para atualizar um colaborador existente.
        /// </summary>
        /// <param name="dal">Instância de DAL para acessar os dados de um colaborador.</param>
        /// <param name="colaboradorRequestEdit">Dados do colaborador a ser atualizada, incluindo o ID do colaborador.</param>
        /// <returns>Retorna 200 (OK) se o colaborador for atualizada com sucesso. Se o colaborador não for encontrada, retorna 404 (Not Found).</returns>
        /// <remarks>Este método utiliza injeção de dependência para obter a instância de DAL. Ele atualiza o nome e os dados existente com os novos dados fornecidos.</remarks>
        grupoBuilder.MapPut("", ([FromServices] DAL<Colaborador> dal, [FromBody] ColaboradorRequestEdit colaboradorRequestEdit) =>
        {
            var colaboradorAtualizado = dal.RecuperarPor(c => c.Id == colaboradorRequestEdit.Id);
            if (colaboradorAtualizado is null)
            {
                return Results.NotFound();
            }
            colaboradorAtualizado.Nome = colaboradorRequestEdit.Nome;
            colaboradorAtualizado.Email = colaboradorRequestEdit.Email;
            colaboradorAtualizado.Telefone = colaboradorRequestEdit.Telefone;

            dal.Atualizar(colaboradorAtualizado);
            return Results.Ok();
        });
    }
    #endregion

    private static ICollection<Cargo> CargoRequestConverter(ICollection<CargoRequest> cargos, DAL<Cargo> dalCargo)
    {
        var listaDeCargos = new List<Cargo>();
        foreach (var item in cargos)
        {
            var entity = RequestToEntity(item);
            var cargo = dalCargo.RecuperarPor(c => c.Nome!.ToUpper().Equals(item.Nome.ToUpper()));
            if (cargo is not null)
            {
                listaDeCargos.Add(cargo);
            }
            else
            {
                listaDeCargos.Add(entity);
            }
        }
        return listaDeCargos;
    }

    private static Cargo RequestToEntity(CargoRequest cargo)
    {
        return new Cargo() { Nome = cargo.Nome };
    }

    private static ICollection<ColaboradorResponse> EntityListToResponseList(IEnumerable<Colaborador> listaDeColaboradores)
    {
        return listaDeColaboradores.Select(c => EntityToResponse(c)).ToList();
    }

    private static ColaboradorResponse EntityToResponse(Colaborador colaborador)
    {
        return new ColaboradorResponse(colaborador.Id, colaborador.Nome!, colaborador.Email!, colaborador.Telefone!, colaborador.Cargo!.Id, colaborador.Cargo.Nome!);
    }
}
