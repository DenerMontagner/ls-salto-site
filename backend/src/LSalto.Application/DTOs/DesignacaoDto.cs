namespace LSalto.Application.DTOs;

public record DesignacaoDto(
    int Id,
    string TipoDesignacao,
    DateOnly Data,
    string PublicadorTitular,
    string? PublicadorAjudante,
    string? Grupo
);
