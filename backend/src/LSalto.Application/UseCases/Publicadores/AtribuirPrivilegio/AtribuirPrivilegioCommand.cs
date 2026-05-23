using MediatR;

namespace LSalto.Application.UseCases.Publicadores.AtribuirPrivilegio;

public record AtribuirPrivilegioCommand(int IdPublicador, int IdPrivilegio, DateOnly DataInicio) : IRequest;
