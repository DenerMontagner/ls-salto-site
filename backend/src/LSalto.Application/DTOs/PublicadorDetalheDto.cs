namespace LSalto.Application.DTOs;

public record PublicadorDetalheDto(
    int Id,
    string Nome,
    string EmailUsername,
    string Sexo,
    bool IsBatizado,
    DateOnly DataNascimento,
    DateOnly? DataBatismo,
    string? Telefone,
    string? Endereco,
    int? CargoAtualId,
    string? CargoAtualNome,
    DateOnly? CargoDataInicio,
    int? PrivilegioAtualId,
    string? PrivilegioAtualNome,
    DateOnly? PrivilegioDataInicio,
    int? GrupoId,
    string? GrupoNome
);
