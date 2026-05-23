using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<Publicador> Publicadores => Set<Publicador>();
    public DbSet<Cargo> Cargos => Set<Cargo>();
    public DbSet<Privilegio> Privilegios => Set<Privilegio>();
    public DbSet<PublicadorCargo> PublicadoresCargos => Set<PublicadorCargo>();
    public DbSet<PublicadorPrivilegio> PublicadoresPrivilegios => Set<PublicadorPrivilegio>();
    public DbSet<Grupo> Grupos => Set<Grupo>();
    public DbSet<GrupoPublicador> GruposPublicadores => Set<GrupoPublicador>();
    public DbSet<TipoDesignacao> TiposDesignacao => Set<TipoDesignacao>();
    public DbSet<Designacao> Designacoes => Set<Designacao>();
    public DbSet<Anuncio> Anuncios => Set<Anuncio>();
    public DbSet<AnexoAnuncio> AnexosAnuncios => Set<AnexoAnuncio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>().HasData(
            new Cargo { Id = 1, NomeCargo = "Ancião" },
            new Cargo { Id = 2, NomeCargo = "Servo Ministerial" }
        );

        modelBuilder.Entity<Privilegio>().HasData(
            new Privilegio { Id = 1, NomePrivilegio = "Pioneiro Regular", RequisitoHoras = 50 },
            new Privilegio { Id = 2, NomePrivilegio = "Pioneiro Auxiliar", RequisitoHoras = 30 }
        );

        modelBuilder.Entity<Grupo>().HasData(
            new Grupo { Id = 1, Nome = "Salto", Local = null, IdAnciaoResponsavel = null },
            new Grupo { Id = 2, Nome = "Itu",   Local = null, IdAnciaoResponsavel = null }
        );

        modelBuilder.Entity<TipoDesignacao>().HasData(
            new TipoDesignacao { Id = 1, Nome = "Presidente", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Anciao },
            new TipoDesignacao { Id = 2, Nome = "Estudo Bíblico (EBC)", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Anciao },
            new TipoDesignacao { Id = 3, Nome = "Discurso Tesouros", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.ServoOuAnciao },
            new TipoDesignacao { Id = 4, Nome = "Discurso Vida Cristã", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.ServoOuAnciao },
            new TipoDesignacao { Id = 5, Nome = "Leitura da Bíblia", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 6, Nome = "Mecânicas", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 7, Nome = "Oração Inicial", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 8, Nome = "Oração Final", RequerSexoMasculino = true, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 9, Nome = "Demonstração (Titular)", RequerSexoMasculino = false, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 10, Nome = "Demonstração (Ajudante)", RequerSexoMasculino = false, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 11, Nome = "Limpeza", RequerSexoMasculino = false, RequerCargoEspecifico = RequisitoCargo.Nenhum },
            new TipoDesignacao { Id = 12, Nome = "Pregação de Campo", RequerSexoMasculino = false, RequerCargoEspecifico = RequisitoCargo.Nenhum }
        );
    }
}
