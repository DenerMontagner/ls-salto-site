using FluentValidation;

namespace LSalto.Application.UseCases.Auth.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email é obrigatório.");
        RuleFor(x => x.Senha).NotEmpty().WithMessage("Senha é obrigatória.");
    }
}
