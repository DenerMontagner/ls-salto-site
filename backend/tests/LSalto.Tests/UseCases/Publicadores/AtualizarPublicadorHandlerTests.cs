using FluentAssertions;
using LSalto.Application.UseCases.Publicadores.Atualizar;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using LSalto.Tests.Helpers;

namespace LSalto.Tests.UseCases.Publicadores;

public class AtualizarPublicadorHandlerTests
{
    private static Publicador PublicadorPadrao(int id = 1, string email = "original@teste.com") =>
        new()
        {
            Id = id, Nome = "Nome Original", EmailUsername = email,
            SenhaHash = "x", Sexo = Sexo.Masculino,
            DataNascimento = new DateOnly(1990, 1, 1)
        };

    private static AtualizarPublicadorCommand ComandoPadrao(int id = 1, string email = "novo@teste.com") => new(
        Id:             id,
        Nome:           "Nome Atualizado",
        EmailUsername:  email,
        Sexo:           "Masculino",
        DataNascimento: new DateOnly(1990, 1, 1),
        DataBatismo:    null,
        Telefone:       null,
        Endereco:       null,
        IdGrupo:        1
    );

    [Fact]
    public async Task Deve_atualizar_dados_do_publicador()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao());
        await ctx.SaveChangesAsync();

        var handler = new AtualizarPublicadorHandler(ctx);
        await handler.Handle(ComandoPadrao(), CancellationToken.None);

        var salvo = ctx.Publicadores.First();
        salvo.Nome.Should().Be("Nome Atualizado");
        salvo.EmailUsername.Should().Be("novo@teste.com");
    }

    [Fact]
    public async Task Deve_trocar_grupo_do_publicador()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao());
        ctx.GruposPublicadores.Add(new GrupoPublicador { IdPublicador = 1, IdGrupo = 1 });
        await ctx.SaveChangesAsync();

        var handler = new AtualizarPublicadorHandler(ctx);
        await handler.Handle(ComandoPadrao() with { IdGrupo = 2 }, CancellationToken.None);

        ctx.GruposPublicadores.Should().ContainSingle(gp => gp.IdGrupo == 2 && gp.IdPublicador == 1);
        ctx.GruposPublicadores.Should().NotContain(gp => gp.IdGrupo == 1);
    }

    [Fact]
    public async Task Deve_lancar_excecao_quando_publicador_nao_encontrado()
    {
        await using var ctx = DbContextFactory.Create();
        var handler = new AtualizarPublicadorHandler(ctx);

        var acao = () => handler.Handle(ComandoPadrao(id: 999), CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*999*");
    }

    [Fact]
    public async Task Deve_lancar_excecao_quando_email_ja_em_uso_por_outro()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao(id: 1, email: "p1@teste.com"));
        ctx.Publicadores.Add(PublicadorPadrao(id: 2, email: "p2@teste.com"));
        await ctx.SaveChangesAsync();

        var handler = new AtualizarPublicadorHandler(ctx);
        // Tenta mudar o email do publicador 1 para o email que já pertence ao publicador 2
        var cmd = ComandoPadrao(id: 1, email: "p2@teste.com");

        var acao = () => handler.Handle(cmd, CancellationToken.None);

        await acao.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("*p2@teste.com*");
    }

    [Fact]
    public async Task Deve_permitir_manter_o_proprio_email()
    {
        await using var ctx = DbContextFactory.Create();
        ctx.Publicadores.Add(PublicadorPadrao(id: 1, email: "mesmo@teste.com"));
        await ctx.SaveChangesAsync();

        var handler = new AtualizarPublicadorHandler(ctx);
        var cmd = ComandoPadrao(id: 1, email: "mesmo@teste.com");

        var acao = () => handler.Handle(cmd, CancellationToken.None);

        await acao.Should().NotThrowAsync();
    }
}
