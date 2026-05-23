using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class PublicadorPrivilegioConfiguration : IEntityTypeConfiguration<PublicadorPrivilegio>
{
    public void Configure(EntityTypeBuilder<PublicadorPrivilegio> builder)
    {
        builder.HasKey(pp => new { pp.IdPublicador, pp.IdPrivilegio, pp.DataInicio });

        builder.HasOne(pp => pp.Publicador)
            .WithMany(p => p.Privilegios)
            .HasForeignKey(pp => pp.IdPublicador)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Privilegio)
            .WithMany(pr => pr.Publicadores)
            .HasForeignKey(pp => pp.IdPrivilegio)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
