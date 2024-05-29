using System.ComponentModel.DataAnnotations;

namespace ControleColaborador.Web.Requests;

public record ColaboradorRequest(
        [Required] string Nome,
        [Required] string Email,
        [Required] string Telefone,
        [Required] int CargoId
    );

