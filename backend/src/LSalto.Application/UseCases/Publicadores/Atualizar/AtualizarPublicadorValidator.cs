using FluentValidation;

namespace LSalto.Application.UseCases.Publicadores.Atualizar;

public class AtualizarPublicadorValidator : AbstractValidator<AtualizarPublicadorCommand>
{
    public AtualizarPublicadorValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
        RuleFor(x => x.EmailUsername).NotEmpty().EmailAddress().WithMessage("Email inválido.");
        RuleFor(x => x.Sexo).Must(s => s == "Masculino" || s == "Feminino")
            .WithMessage("Sexo deve ser 'Masculino' ou 'Feminino'.");
        RuleFor(x => x.IdGrupo)
            .Must(id => id.HasValue && id.Value > 0)
            .WithMessage("Grupo é obrigatório.");
    }
}
