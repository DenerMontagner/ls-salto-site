namespace LSalto.Domain.Entities;

public class Privilegio
{
    public int Id { get; set; }
    public string NomePrivilegio { get; set; } = string.Empty;
    public int? RequisitoHoras { get; set; }

    public ICollection<PublicadorPrivilegio> Publicadores { get; set; } = [];
}
