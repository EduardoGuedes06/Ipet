
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class CarrinhoProdutoMapping : IEntityTypeConfiguration<CarrinhoProduto>
    {
        public void Configure(EntityTypeBuilder<CarrinhoProduto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Carrinho)
                .WithMany(c => c.CarrinhoProdutos)
                .HasForeignKey(p => p.CarrinhoId);

            builder.HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(p => p.ProdutoId);

            builder.Property(p => p.Quantidade)
                .IsRequired();

            builder.ToTable("CarrinhoProduto");
        }
    }
}