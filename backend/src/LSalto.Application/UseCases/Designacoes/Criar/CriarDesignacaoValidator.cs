using FluentValidation;

namespace LSalto.Application.UseCases.Designacoes.Criar;

public class CriarDesignacaoValidator : AbstractValidator<CriarDesignacaoCommand>
{
    public CriarDesignacaoValidator()
    {
        RuleFor(x => x.IdTipoDesignacao).GreaterThan(0).WithMessage("Tipo de designação é obrigatório.");
        RuleFor(x => x.IdPublicadorTitular).GreaterThan(0).WithMessage("Publicador titular é obrigatório.");
        RuleFor(x => x.Data).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Não é possível criar designações em datas passadas.");
    }
}
