using FluentAssertions;
using LSalto.Application.Common.Interfaces;
using LSalto.Application.UseCases.Auth.Login;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using LSalto.Tests.Helpers;
using NSubstitute;

namespace LSalto.Tests.UseCases.Auth;

public class LoginHandlerTests
{
    private readonly IPasswordService _passwords = Substitute.For<IPasswordService>();

    private static Publicador PublicadorPadrao(string email = "user@teste.com") =>
        new()
        {
            Id = 1, Nome = "Usuário Teste", EmailUsername = email,
            SenhaHash = "hash_correta", Sexo = Sexo.Masculino,
            DataNascimento = new DateOnly(1990, 1, 1)
        };

    [Fact]
    public async Task Deve_retornar_role_Publicador_quando_sem_cargo()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao());
        await ctx.SaveChangesAsync();

        _passwords.Verify("senha_certa", "hash_correta").Returns(true);

        var handler = new LoginHandler(ctx, _passwords);
        var resultado = await handler.Handle(new LoginCommand("user@teste.com", "senha_certa"), CancellationToken.None);

        resultado.Role.Should().Be("Publicador");
        resultado.Nome.Should().Be("Usuário Teste");
    }

    [Fact]
    public async Task Deve_retornar_role_Anciao_quando_possui_cargo_ativo()
    {
        await using var ctx = DbContextFactory.Create();
        var pub = PublicadorPadrao();
        pub.Cargos.Add(new PublicadorCargo { IdPublicador = 1, IdCargo = 1, DataInicio = new DateOnly(2020, 1, 1), DataFim = null });
        ctx.Publicadores.Add(pub);
        await ctx.SaveChangesAsync();

        _passwords.Verify("senha_certa", "hash_correta").Returns(true);

        var handler = new LoginHandler(ctx, _passwords);
        var resultado = await handler.Handle(new LoginCommand("user@teste.com", "senha_certa"), CancellationToken.None);

        resultado.Role.Should().Be("Anciao");
    }

    [Fact]
    public async Task Deve_lancar_excecao_quando_email_nao_existe()
    {
        await using var ctx = DbContextFactory.Create();
        var handler = new LoginHandler(ctx, _passwords);

        var acao = () => handler.Handle(new LoginCommand("naoexiste@teste.com", "qualquer"), CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*inválidos*");
    }

    [Fact]
    public async Task Deve_lancar_excecao_quando_senha_esta_errada()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao());
        await ctx.SaveChangesAsync();

        _passwords.Verify("senha_errada", "hash_correta").Returns(false);

        var handler = new LoginHandler(ctx, _passwords);

        var acao = () => handler.Handle(new LoginCommand("user@teste.com", "senha_errada"), CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*inválidos*");
    }

    [Fact]
    public async Task Nao_deve_considerar_cargo_encerrado_como_anciao()
    {
        await using var ctx = DbContextFactory.Create();
        var pub = PublicadorPadrao();
        pub.Cargos.Add(new PublicadorCargo
        {
            IdPublicador = 1, IdCargo = 1,
            DataInicio = new DateOnly(2015, 1, 1),
            DataFim = new DateOnly(2020, 1, 1) // cargo encerrado
        });
        ctx.Publicadores.Add(pub);
        await ctx.SaveChangesAsync();

        _passwords.Verify("senha_certa", "hash_correta").Returns(true);

        var handler = new LoginHandler(ctx, _passwords);
        var resultado = await handler.Handle(new LoginCommand("user@teste.com", "senha_certa"), CancellationToken.None);

        resultado.Role.Should().Be("Publicador");
    }
}
