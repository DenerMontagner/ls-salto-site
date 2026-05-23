using MediatR;

namespace LSalto.Application.UseCases.Anuncios.Criar;

public record CriarAnuncioCommand(string Descricao) : IRequest<int>;
