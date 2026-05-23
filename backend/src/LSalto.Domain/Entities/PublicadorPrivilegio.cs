namespace LSalto.Domain.Entities;

public class PublicadorPrivilegio
{
    public int IdPublicador { get; set; }
    public Publicador Publicador { get; set; } = null!;

    public int IdPrivilegio { get; set; }
    public Privilegio Privilegio { get; set; } = null!;

    public DateOnly DataInicio { get; set; }
    public DateOnly? DataFim { get; set; }
}
