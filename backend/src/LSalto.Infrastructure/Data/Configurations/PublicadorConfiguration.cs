using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LSalto.Infrastructure.Data.Configurations;

public class PublicadorConfiguration : IEntityTypeConfiguration<Publicador>
{
    public void Configure(EntityTypeBuilder<Publicador> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(150);
        builder.Property(p => p.EmailUsername).IsRequired().HasMaxLength(200);
        builder.Property(p => p.SenhaHash).IsRequired();
        builder.Property(p => p.Telefone).HasMaxLength(20);
        builder.Property(p => p.Endereco).HasMaxLength(300);

        builder.Property(p => p.Sexo)
            .IsRequired()
            .HasConversion(
                v => v == Sexo.Masculino ? "M" : "F",
                v => v == "M" ? Sexo.Masculino : Sexo.Feminino)
            .HasColumnType("varchar(1)");

        builder.HasIndex(p => p.EmailUsername).IsUnique();

        builder.HasMany(p => p.DesignacoesComoTitular)
            .WithOne(d => d.PublicadorTitular)
            .HasForeignKey(d => d.IdPublicadorTitular)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.DesignacoesComoAjudante)
            .WithOne(d => d.PublicadorAjudante)
            .HasForeignKey(d => d.IdPublicadorAjudante)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
