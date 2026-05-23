namespace LSalto.Application.DTOs;

public record AnuncioComAnexosDto(
    int Id,
    string Descricao,
    DateTime DataCriacao,
    List<string> Anexos
);
