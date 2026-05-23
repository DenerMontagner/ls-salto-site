using FluentValidation;

namespace LSalto.Application.UseCases.Publicadores.Criar;

public class CriarPublicadorValidator : AbstractValidator<CriarPublicadorCommand>
{
    public CriarPublicadorValidator()
    {
        RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
        RuleFor(x => x.EmailUsername).NotEmpty().EmailAddress().WithMessage("Email inválido.");
        RuleFor(x => x.Senha).MinimumLength(6).WithMessage("Senha deve ter ao menos 6 caracteres.");
        RuleFor(x => x.Sexo).Must(s => s == "Masculino" || s == "Feminino")
            .WithMessage("Sexo deve ser 'Masculino' ou 'Feminino'.");
        RuleFor(x => x.IdGrupo)
            .Must(id => id.HasValue && id.Value > 0)
            .WithMessage("Grupo é obrigatório.");
    }
}
