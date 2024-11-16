using Microsoft.EntityFrameworkCore;
using Models.Configuracao; 
using Models.Resultados;
namespace Data
{
    public class EleicaoContext : DbContext
    {
        public DbSet<ResultadosEleicao> ResultadosEleicao {get;set;}
        public DbSet<Eleicao> Eleicoes { get; set; }
        public DbSet<ZonaEleitoral> ZonaEleitoral { get; set; }
        public DbSet<Secao> Secao { get; set; } 

        public EleicaoContext(DbContextOptions<EleicaoContext> options) : base(options)  
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=eleicao.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
