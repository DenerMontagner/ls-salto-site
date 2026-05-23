namespace LSalto.Domain.Entities;

public class PublicadorCargo
{
    public int IdPublicador { get; set; }
    public Publicador Publicador { get; set; } = null!;

    public int IdCargo { get; set; }
    public Cargo Cargo { get; set; } = null!;

    public DateOnly DataInicio { get; set; }
    public DateOnly? DataFim { get; set; }
}
