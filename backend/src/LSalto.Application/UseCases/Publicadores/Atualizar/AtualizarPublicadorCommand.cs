using MediatR;

namespace LSalto.Application.UseCases.Publicadores.Atualizar;

public record AtualizarPublicadorCommand(
    int Id,
    string Nome,
    string EmailUsername,
    string Sexo,
    DateOnly DataNascimento,
    DateOnly? DataBatismo,
    string? Telefone,
    string? Endereco,
    int? IdGrupo
) : IRequest;
