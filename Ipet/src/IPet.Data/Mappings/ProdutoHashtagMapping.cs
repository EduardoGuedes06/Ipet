
using Ipet.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ipet.Data.Mappings
{
    public class ProdutoHashtagMapping : IEntityTypeConfiguration<ProdutoHashtag>
    {
        public void Configure(EntityTypeBuilder<ProdutoHashtag> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Tag)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasConversion(tag => tag.ToUpper(), tag => tag);

            builder.ToTable("ProdutoHashtag");
        }
    }
}