
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Imagem)
                //.IsRequired()
                .HasColumnType("LONGTEXT");
            builder.Property(p => p.Estabelecimento);
            //.IsRequired()
            builder.HasMany(p => p.Hashtags)
                .WithOne(h => h.Produto)
                .HasForeignKey(h => h.IdProduto)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Avaliacoes)
                .WithOne(h => h.Produto)
                .HasForeignKey(h => h.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Produtos");
        }
    }
}