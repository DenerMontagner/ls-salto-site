using LSalto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Publicador> Publicadores { get; }
    DbSet<PublicadorCargo> PublicadoresCargos { get; }
    DbSet<PublicadorPrivilegio> PublicadoresPrivilegios { get; }
    DbSet<TipoDesignacao> TiposDesignacao { get; }
    DbSet<Designacao> Designacoes { get; }
    DbSet<Anuncio> Anuncios { get; }
    DbSet<Grupo> Grupos { get; }
    DbSet<GrupoPublicador> GruposPublicadores { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
