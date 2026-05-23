using LSalto.Domain.Enums;

namespace LSalto.Domain.Entities;

public class TipoDesignacao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool RequerSexoMasculino { get; set; }
    public RequisitoCargo RequerCargoEspecifico { get; set; } = RequisitoCargo.Nenhum;

    public ICollection<Designacao> Designacoes { get; set; } = [];
}
