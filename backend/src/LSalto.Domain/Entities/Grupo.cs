namespace LSalto.Domain.Entities;

public class Grupo
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Local { get; set; }
    public int? IdAnciaoResponsavel { get; set; }
    public Publicador? AnciaoResponsavel { get; set; }

    public ICollection<GrupoPublicador> Publicadores { get; set; } = [];
    public ICollection<Designacao> Designacoes { get; set; } = [];
}
