using Microsoft.EntityFrameworkCore;
using Models.Configuracao; 
namespace Data
{
    public class EleicaoContext : DbContext
    {
        public DbSet<Eleicao> Eleicoes { get; set; }
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
