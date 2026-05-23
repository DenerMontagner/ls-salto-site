namespace LSalto.Domain.Entities;

public class Designacao
{
    public int Id { get; set; }

    public int IdTipoDesignacao { get; set; }
    public TipoDesignacao TipoDesignacao { get; set; } = null!;

    public DateOnly Data { get; set; }

    public int IdPublicadorTitular { get; set; }
    public Publicador PublicadorTitular { get; set; } = null!;

    public int? IdPublicadorAjudante { get; set; }
    public Publicador? PublicadorAjudante { get; set; }

    public int? IdGrupo { get; set; }
    public Grupo? Grupo { get; set; }
}
