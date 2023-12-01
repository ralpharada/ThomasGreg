using ThomasGreg.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ThomasGreg.Infra.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Logradouro> Logradouros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.Token); // Chave primária
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Nome).HasMaxLength(50).IsUnicode(false).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
                entity.Property(e => e.Senha).HasMaxLength(50).IsUnicode(false).IsRequired();
            });
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.HasMany(e => e.Logradouros) // Um cliente possui muitos logradouros
                    .WithOne(logradouro => logradouro.Cliente) // Um logradouro pertence a um cliente
                    .HasForeignKey(logradouro => logradouro.ClienteId) // Chave estrangeira no logradouro
                    .OnDelete(DeleteBehavior.Cascade); // Configuração para excluir em cascata
            });

            modelBuilder.Entity<Logradouro>(entity =>
            {
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.ClienteId); // Chave estrangeira
                entity.HasOne(e => e.Cliente) // Um logradouro pertence a um cliente
                    .WithMany(cliente => cliente.Logradouros) // Um cliente possui muitos logradouros
                    .HasForeignKey(e => e.ClienteId); // Chave estrangeira no logradouro
            });

        }
    }
}
