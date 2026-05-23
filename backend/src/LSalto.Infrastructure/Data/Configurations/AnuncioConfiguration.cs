using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class AnuncioConfiguration : IEntityTypeConfiguration<Anuncio>
{
    public void Configure(EntityTypeBuilder<Anuncio> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Descricao).IsRequired().HasMaxLength(2000);

        builder.HasMany(a => a.Anexos)
            .WithOne(x => x.Anuncio)
            .HasForeignKey(x => x.IdAnuncio)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
