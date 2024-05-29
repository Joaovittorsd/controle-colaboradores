using System.ComponentModel.DataAnnotations;

namespace ControleColaborador.API.Requests;

public record ColaboradorRequest(
        [Required] string Nome,
        [Required] string Email,
        [Required] string Telefone,
        [Required] int CargoId
    );

