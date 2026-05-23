using MediatR;

namespace LSalto.Application.UseCases.Publicadores.Criar;

public record CriarPublicadorCommand(
    string Nome,
    string EmailUsername,
    string Senha,
    string Sexo,
    DateOnly DataNascimento,
    DateOnly? DataBatismo,
    string? Telefone,
    string? Endereco,
    int? IdGrupo
) : IRequest<int>;
