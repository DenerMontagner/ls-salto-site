using MediatR;

namespace LSalto.Application.UseCases.Publicadores.AtribuirCargo;

public record AtribuirCargoCommand(int IdPublicador, int IdCargo, DateOnly DataInicio) : IRequest;
