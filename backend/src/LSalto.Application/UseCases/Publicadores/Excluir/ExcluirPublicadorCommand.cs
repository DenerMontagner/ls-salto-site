using MediatR;

namespace LSalto.Application.UseCases.Publicadores.Excluir;

public record ExcluirPublicadorCommand(int Id) : IRequest;
