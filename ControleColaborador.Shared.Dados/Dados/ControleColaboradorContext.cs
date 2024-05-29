using ControleColaborador.Shared.Modelos.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ControleColaborador.Shared.Dados.Dados;

public class ControleColaboradorContext : DbContext
{
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Cargo> Cargos { get; set; }

    private readonly string connectionString = "server=auth-db1073.hstgr.io;uid=u327172299_teste;pwd=@Ney91249045;database=u327172299_teste";

    public ControleColaboradorContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        optionsBuilder
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .UseLazyLoadingProxies();
    }
}
