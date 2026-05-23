using MediatR;

namespace LSalto.Application.UseCases.Publicadores.RemoverCargo;

public record RemoverCargoCommand(int IdPublicador) : IRequest;
