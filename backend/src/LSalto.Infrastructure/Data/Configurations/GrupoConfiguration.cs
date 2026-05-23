using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
{
    public void Configure(EntityTypeBuilder<Grupo> builder)
    {
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Nome).IsRequired().HasMaxLength(100);
        builder.Property(g => g.Local).HasMaxLength(200);

        builder.HasOne(g => g.AnciaoResponsavel)
            .WithMany()
            .HasForeignKey(g => g.IdAnciaoResponsavel)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
