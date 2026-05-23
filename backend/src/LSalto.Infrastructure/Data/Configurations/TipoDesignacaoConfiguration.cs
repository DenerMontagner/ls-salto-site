using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class TipoDesignacaoConfiguration : IEntityTypeConfiguration<TipoDesignacao>
{
    public void Configure(EntityTypeBuilder<TipoDesignacao> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome).IsRequired().HasMaxLength(100);

        builder.Property(t => t.RequerCargoEspecifico)
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<RequisitoCargo>(v))
            .HasColumnType("varchar(20)");
    }
}
