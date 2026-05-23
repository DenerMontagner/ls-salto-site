using FluentAssertions;
using LSalto.Application.UseCases.Publicadores.Criar;

namespace LSalto.Tests.UseCases.Publicadores;

public class CriarPublicadorValidatorTests
{
    private readonly CriarPublicadorValidator _validator = new();

    private static CriarPublicadorCommand ComandoValido() => new(
        Nome:           "João Silva",
        EmailUsername:  "joao@teste.com",
        Senha:          "senha123",
        Sexo:           "Masculino",
        DataNascimento: new DateOnly(1990, 1, 1),
        DataBatismo:    null,
        Telefone:       null,
        Endereco:       null,
        IdGrupo:        1
    );

    [Fact]
    public async Task Deve_ser_valido_com_dados_corretos()
    {
        var resultado = await _validator.ValidateAsync(ComandoValido());
        resultado.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task Deve_falhar_quando_nome_esta_vazio(string nome)
    {
        var cmd = ComandoValido() with { Nome = nome };
        var resultado = await _validator.ValidateAsync(cmd);
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "Nome");
    }

    [Theory]
    [InlineData("nao-e-email")]
    [InlineData("semdominio@")]
    [InlineData("")]
    public async Task Deve_falhar_quando_email_e_invalido(string email)
    {
        var cmd = ComandoValido() with { EmailUsername = email };
        var resultado = await _validator.ValidateAsync(cmd);
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "EmailUsername");
    }

    [Fact]
    public async Task Deve_falhar_quando_senha_tem_menos_de_6_caracteres()
    {
        var cmd = ComandoValido() with { Senha = "abc" };
        var resultado = await _validator.ValidateAsync(cmd);
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "Senha");
    }

    [Theory]
    [InlineData("masculino")]
    [InlineData("F")]
    [InlineData("Outro")]
    public async Task Deve_falhar_quando_sexo_e_invalido(string sexo)
    {
        var cmd = ComandoValido() with { Sexo = sexo };
        var resultado = await _validator.ValidateAsync(cmd);
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "Sexo");
    }

    [Fact]
    public async Task Deve_falhar_quando_grupo_e_nulo()
    {
        var cmd = ComandoValido() with { IdGrupo = null };
        var resultado = await _validator.ValidateAsync(cmd);
        resultado.IsValid.Should().BeFalse();
        resultado.Errors.Should().Contain(e => e.PropertyName == "IdGrupo");
    }
}
