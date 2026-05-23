namespace LSalto.Domain.Entities;

public class Cargo
{
    public int Id { get; set; }
    public string NomeCargo { get; set; } = string.Empty;

    public ICollection<PublicadorCargo> Publicadores { get; set; } = [];
}
