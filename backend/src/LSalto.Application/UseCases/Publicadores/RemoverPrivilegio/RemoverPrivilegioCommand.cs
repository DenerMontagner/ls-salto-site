using MediatR;

namespace LSalto.Application.UseCases.Publicadores.RemoverPrivilegio;

public record RemoverPrivilegioCommand(int IdPublicador) : IRequest;
