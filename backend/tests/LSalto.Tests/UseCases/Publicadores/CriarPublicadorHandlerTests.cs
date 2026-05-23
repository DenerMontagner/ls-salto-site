using FluentAssertions;
using LSalto.Application.Common.Interfaces;
using LSalto.Application.UseCases.Publicadores.Criar;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using LSalto.Tests.Helpers;
using NSubstitute;

namespace LSalto.Tests.UseCases.Publicadores;

public class CriarPublicadorHandlerTests
{
    private readonly IPasswordService _passwords = Substitute.For<IPasswordService>();

    public CriarPublicadorHandlerTests()
    {
        _passwords.Hash(Arg.Any<string>()).Returns("hash_fake");
    }

    private static CriarPublicadorCommand ComandoValido(int? idGrupo = 1) => new(
        Nome:           "Maria Souza",
        EmailUsername:  "maria@teste.com",
        Senha:          "senha123",
        Sexo:           "Feminino",
        DataNascimento: new DateOnly(1985, 6, 15),
        DataBatismo:    new DateOnly(2005, 3, 10),
        Telefone:       null,
        Endereco:       null,
        IdGrupo:        idGrupo
    );

    [Fact]
    public async Task Deve_criar_publicador_e_retornar_id()
    {
        await using var ctx = DbContextFactory.Create();
        var handler = new CriarPublicadorHandler(ctx, _passwords);

        var id = await handler.Handle(ComandoValido(), CancellationToken.None);

        id.Should().BeGreaterThan(0);
        ctx.Publicadores.Should().ContainSingle(p => p.EmailUsername == "maria@teste.com");
    }

    [Fact]
    public async Task Deve_salvar_hash_da_senha()
    {
        await using var ctx = DbContextFactory.Create();
        var handler = new CriarPublicadorHandler(ctx, _passwords);

        await handler.Handle(ComandoValido(), CancellationToken.None);

        ctx.Publicadores.First().SenhaHash.Should().Be("hash_fake");
    }

    [Fact]
    public async Task Deve_associar_grupo_quando_idGrupo_informado()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Grupos.Add(new Grupo { Id = 1, Nome = "Salto" });
        await ctx.SaveChangesAsync();

        var handler = new CriarPublicadorHandler(ctx, _passwords);
        var id = await handler.Handle(ComandoValido(idGrupo: 1), CancellationToken.None);

        ctx.GruposPublicadores
            .Should().ContainSingle(gp => gp.IdPublicador == id && gp.IdGrupo == 1);
    }

    [Fact]
    public async Task Nao_deve_criar_grupo_publicador_quando_idGrupo_e_nulo()
    {
        await using var ctx = DbContextFactory.Create();
        var handler = new CriarPublicadorHandler(ctx, _passwords);

        await handler.Handle(ComandoValido(idGrupo: null), CancellationToken.None);

        ctx.GruposPublicadores.Should().BeEmpty();
    }

    [Fact]
    public async Task Deve_lancar_excecao_quando_email_ja_existe()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(new Publicador
        {
            Nome = "Outro", EmailUsername = "maria@teste.com",
            SenhaHash = "x", Sexo = Sexo.Feminino,
            DataNascimento = new DateOnly(1980, 1, 1)
        });
        await ctx.SaveChangesAsync();

        var handler = new CriarPublicadorHandler(ctx, _passwords);

        var acao = () => handler.Handle(ComandoValido(), CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*maria@teste.com*");
    }
}
