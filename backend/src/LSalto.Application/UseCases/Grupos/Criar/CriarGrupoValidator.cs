using FluentValidation;

namespace LSalto.Application.UseCases.Grupos.Criar;

public class CriarGrupoValidator : AbstractValidator<CriarGrupoCommand>
{
    public CriarGrupoValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome do grupo é obrigatório.");
    }
}
