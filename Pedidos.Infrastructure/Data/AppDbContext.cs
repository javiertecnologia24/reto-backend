using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;

namespace Pedidos.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(e =>
            {
                e.HasIndex(p => p.NumeroPedido).IsUnique();

                e.Property(p => p.NumeroPedido)
                .HasMaxLength(50)
                .IsRequired();
              
                e.Property(p => p.Cliente)
                .HasMaxLength(200)
                .IsRequired();

                e.Property(p => p.Estado)
                .HasColumnType("char(1)")
                .HasDefaultValue("R")
                .IsRequired();
            });
        }
    }
}
