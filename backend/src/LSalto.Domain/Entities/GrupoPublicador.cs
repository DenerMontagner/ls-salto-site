namespace LSalto.Domain.Entities;

public class GrupoPublicador
{
    public int IdGrupo { get; set; }
    public Grupo Grupo { get; set; } = null!;

    public int IdPublicador { get; set; }
    public Publicador Publicador { get; set; } = null!;
}
