using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class AnexoAnuncioConfiguration : IEntityTypeConfiguration<AnexoAnuncio>
{
    public void Configure(EntityTypeBuilder<AnexoAnuncio> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CaminhoArquivoUrl).IsRequired().HasMaxLength(500);
    }
}
