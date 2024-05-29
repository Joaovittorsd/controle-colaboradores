namespace ControleColaborador.Shared.Modelos.Modelos;

public class Colaborador
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public int CargoId { get; set; }

    public virtual Cargo? Cargo { get; set; }

    public Colaborador(string nome)
    {
        Nome = nome;
    }
}
