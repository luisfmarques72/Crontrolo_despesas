using ControloDespesas.Mapeamento;
using Microsoft.EntityFrameworkCore;

namespace ControloDespesas.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Meses> Meses { get; set; }
        public DbSet<Salarios> Salarios { get; set; }
        public DbSet<Despesas> Despesas { get; set; }
        public DbSet<TipoDespesas> TipoDespesas { get; set; }
        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes) { }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoDesepsasMap());
            modelBuilder.ApplyConfiguration(new DespesasMap());
            modelBuilder.ApplyConfiguration(new SalariosMap());
            modelBuilder.ApplyConfiguration(new MesesMap());
        }
    }
}
