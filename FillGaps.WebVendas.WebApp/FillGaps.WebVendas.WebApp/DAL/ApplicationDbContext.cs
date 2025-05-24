using Microsoft.EntityFrameworkCore;
using FillGaps.WebVendas.WebApp.Entities;

namespace FillGaps.WebVendas.WebApp.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Entities.Cliente> Cliente { get; set; }
        public DbSet<Entities.Documento> Documento { get; set; }
        public DbSet<Entities.DocumentoItem> DocumentoItem { get; set; }
        public DbSet<Entities.Produto> Produto { get; set; }
        public DbSet<Entities.Categoria> Categoria { get; set; }
        public DbSet<Entities.Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entities.Documento>()
                .HasAlternateKey(d => d.NumeroDocumento);
            modelBuilder.Entity<Entities.Documento>()
                .HasOne(d => d.Cliente)
                .WithMany(c => c.Documentos)
                .HasForeignKey(d => d.IdCliente);

            modelBuilder.Entity<Entities.DocumentoItem>()
                .HasKey(e => new { e.IdDocumento, e.IdProduto });
            modelBuilder.Entity<Entities.DocumentoItem>()
                .HasOne(e => e.Documento)
                .WithMany(p => p.DocumentoItens)
                .HasForeignKey(d => d.IdDocumento);
            modelBuilder.Entity<Entities.DocumentoItem>()
                .HasOne(e => e.Produto)
                .WithMany(p => p.DocumentoItens)
                .HasForeignKey(d => d.IdProduto);

            modelBuilder.Entity<Entities.Cliente>()
                .HasAlternateKey(c => c.CPF_CNPJ);
            modelBuilder.Entity<Entities.Cliente>()
                .HasAlternateKey(c => c.CodigoCliente);

            modelBuilder.Entity<Entities.Categoria>()
                .HasAlternateKey(c => c.CodigoCategoria);

            modelBuilder.Entity<Entities.Produto>()
                .HasAlternateKey(p => p.CodigoProduto);
            modelBuilder.Entity<Entities.Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.IdCategoria);

            modelBuilder.Entity<Entities.Usuario>()
                .HasAlternateKey(u => u.LoginUsuario);
            modelBuilder.Entity<Entities.Usuario>()
                .HasAlternateKey(u => u.EmailUsuario);
        }
    }
}
