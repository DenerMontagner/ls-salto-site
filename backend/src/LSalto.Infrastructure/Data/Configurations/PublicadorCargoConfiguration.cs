using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class PublicadorCargoConfiguration : IEntityTypeConfiguration<PublicadorCargo>
{
    public void Configure(EntityTypeBuilder<PublicadorCargo> builder)
    {
        builder.HasKey(pc => new { pc.IdPublicador, pc.IdCargo, pc.DataInicio });

        builder.HasOne(pc => pc.Publicador)
            .WithMany(p => p.Cargos)
            .HasForeignKey(pc => pc.IdPublicador)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.Cargo)
            .WithMany(c => c.Publicadores)
            .HasForeignKey(pc => pc.IdCargo)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
