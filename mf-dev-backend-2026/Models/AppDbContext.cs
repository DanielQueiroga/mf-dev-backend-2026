using Microsoft.EntityFrameworkCore;

namespace mf_dev_backend_2026.Models
{
    // Responsavel por fazer a configuração do Entity FrameWork
    // Transformar todas as classes e gerar as tabelas
    public class AppDbContext : DbContext 
    {
        // Construtor da aplicação
        // Passa as options(opções) para ele configurar o banco
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {}

        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
