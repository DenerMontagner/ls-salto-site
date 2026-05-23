using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class GrupoPublicadorConfiguration : IEntityTypeConfiguration<GrupoPublicador>
{
    public void Configure(EntityTypeBuilder<GrupoPublicador> builder)
    {
        builder.HasKey(gp => new { gp.IdGrupo, gp.IdPublicador });

        builder.HasOne(gp => gp.Grupo)
            .WithMany(g => g.Publicadores)
            .HasForeignKey(gp => gp.IdGrupo)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(gp => gp.Publicador)
            .WithMany(p => p.Grupos)
            .HasForeignKey(gp => gp.IdPublicador)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
