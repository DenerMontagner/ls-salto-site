using LSalto.Domain.Enums;

namespace LSalto.Domain.Entities;

public class Publicador
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string EmailUsername { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public DateOnly DataNascimento { get; set; }
    public Sexo Sexo { get; set; }
    public DateOnly? DataBatismo { get; set; }
    public string? Telefone { get; set; }
    public string? Endereco { get; set; }

    public ICollection<PublicadorCargo> Cargos { get; set; } = [];
    public ICollection<PublicadorPrivilegio> Privilegios { get; set; } = [];
    public ICollection<GrupoPublicador> Grupos { get; set; } = [];
    public ICollection<Designacao> DesignacoesComoTitular { get; set; } = [];
    public ICollection<Designacao> DesignacoesComoAjudante { get; set; } = [];
}
