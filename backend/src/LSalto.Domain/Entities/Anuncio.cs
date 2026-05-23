namespace LSalto.Domain.Entities;

public class Anuncio
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    public ICollection<AnexoAnuncio> Anexos { get; set; } = [];
}
