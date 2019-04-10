using Microsoft.EntityFrameworkCore;

namespace OpenServices.Entities
{
    public class OpenServicesContext : DbContext
    {
        public OpenServicesContext(DbContextOptions<OpenServicesContext> options)
           : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PrestadorServico> PrestadorServicos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<FormaPagamento> FormaPagamentos { get; set; }
        public DbSet<CategoriaPrestador> CategoriaPrestadors { get; set; }
    }
}