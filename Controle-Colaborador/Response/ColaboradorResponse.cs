namespace ControleColaborador.API.Response;

public record ColaboradorResponse(int Id, string Nome, string Email, string Telefone, int CargoId, string NomeCargo);
