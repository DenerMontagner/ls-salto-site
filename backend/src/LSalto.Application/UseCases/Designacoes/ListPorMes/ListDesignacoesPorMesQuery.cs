using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Designacoes.ListPorMes;

public record ListDesignacoesPorMesQuery(int Ano, int Mes) : IRequest<List<DesignacaoDto>>;
