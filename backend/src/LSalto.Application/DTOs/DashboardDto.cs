namespace LSalto.Application.DTOs;

public record DashboardDto(
    List<DesignacaoDto> ProximasDesignacoes,
    List<AnuncioDto> UltimosAnuncios
);
