namespace LSalto.Domain.Entities;

public class AnexoAnuncio
{
    public int Id { get; set; }
    public int IdAnuncio { get; set; }
    public Anuncio Anuncio { get; set; } = null!;
    public string CaminhoArquivoUrl { get; set; } = string.Empty;
}
