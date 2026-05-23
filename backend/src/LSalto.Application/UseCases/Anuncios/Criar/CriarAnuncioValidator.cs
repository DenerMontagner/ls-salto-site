using FluentValidation;

namespace LSalto.Application.UseCases.Anuncios.Criar;

public class CriarAnuncioValidator : AbstractValidator<CriarAnuncioCommand>
{
    public CriarAnuncioValidator()
    {
        RuleFor(x => x.Descricao).NotEmpty().WithMessage("Descrição do anúncio é obrigatória.");
    }
}
