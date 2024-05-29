using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ControleColaborador.API.Requests;

public record ColaboradorRequestEdit(
        int Id,
        [property: JsonPropertyName("nome")][Required] string Nome,
        [property: JsonPropertyName("email")][Required] string Email,
        [property: JsonPropertyName("telefone")][Required] string Telefone,
        [property: JsonPropertyName("cargo_id")][Required] int CargoId
    ) : ColaboradorRequest(Nome, Email, Telefone, CargoId);

