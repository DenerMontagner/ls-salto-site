using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class DesignacaoConfiguration : IEntityTypeConfiguration<Designacao>
{
    public void Configure(EntityTypeBuilder<Designacao> builder)
    {
        builder.HasKey(d => d.Id);

        builder.HasOne(d => d.TipoDesignacao)
            .WithMany(t => t.Designacoes)
            .HasForeignKey(d => d.IdTipoDesignacao)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.PublicadorTitular)
            .WithMany(p => p.DesignacoesComoTitular)
            .HasForeignKey(d => d.IdPublicadorTitular)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.PublicadorAjudante)
            .WithMany(p => p.DesignacoesComoAjudante)
            .HasForeignKey(d => d.IdPublicadorAjudante)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.Grupo)
            .WithMany(g => g.Designacoes)
            .HasForeignKey(d => d.IdGrupo)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(d => new { d.IdPublicadorTitular, d.Data, d.IdTipoDesignacao })
            .HasDatabaseName("IX_Designacao_Conflito");
    }
}
