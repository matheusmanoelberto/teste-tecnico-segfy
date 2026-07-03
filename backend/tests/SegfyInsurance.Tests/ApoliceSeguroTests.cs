using SegfyInsurance.Domain.Entities;
using SegfyInsurance.Domain.Enums;
using SegfyInsurance.Domain.Exceptions;
using SegfyInsurance.Domain.ValueObjects;

namespace SegfyInsurance.Tests;

public class ApoliceSeguroTests
{
    [Fact]
    public void Criar_DeveCriarApoliceValida()
    {
        var apolice = CriarApolice();

        Assert.NotEqual(Guid.Empty, apolice.Id);
        Assert.Equal("SEG-2026-0001", apolice.NumeroApolice.Valor);
        Assert.Equal("12345678901", apolice.DocumentoSegurado.Valor);
        Assert.Equal("ABC1D23", apolice.PlacaVeiculo.Valor);
        Assert.Equal(SituacaoApolice.Ativa, apolice.Situacao);
    }

    [Fact]
    public void NumeroApolice_DeveGerarNoPadraoEsperado()
    {
        var numeroApolice = NumeroApolice.Gerar(2026, 12);

        Assert.Equal("SEG-2026-0012", numeroApolice.Valor);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Dinheiro_NaoDevePermitirValorZeroOuNegativo(decimal valor)
    {
        Assert.Throws<ExcecaoDominio>(() => new Dinheiro(valor));
    }

    [Fact]
    public void Criar_NaoDevePermitirVigenciaInvalida()
    {
        Assert.Throws<ExcecaoDominio>(() =>
            ApoliceSeguro.Criar(
                NumeroApolice.Gerar(2026, 1),
                new Documento("12345678901"),
                new PlacaVeiculo("ABC1D23"),
                new Dinheiro(100),
                new DateOnly(2026, 8, 1),
                new DateOnly(2026, 7, 1)));
    }

    [Theory]
    [InlineData("123")]
    [InlineData("")]
    public void Documento_DeveValidarCpfOuCnpj(string documento)
    {
        Assert.Throws<ExcecaoDominio>(() => new Documento(documento));
    }

    [Theory]
    [InlineData("ABC1234", "ABC1234", TipoPlaca.Antiga)]
    [InlineData("ABC-1234", "ABC1234", TipoPlaca.Antiga)]
    [InlineData("abc1d23", "ABC1D23", TipoPlaca.Mercosul)]
    [InlineData("ABC 1D23", "ABC1D23", TipoPlaca.Mercosul)]
    public void PlacaVeiculo_DeveNormalizarEIdentificarTipo(string placa, string placaEsperada, TipoPlaca tipoEsperado)
    {
        var placaVeiculo = new PlacaVeiculo(placa);

        Assert.Equal(placaEsperada, placaVeiculo.Valor);
        Assert.Equal(tipoEsperado, placaVeiculo.Tipo);
    }

    [Theory]
    [InlineData("")]
    [InlineData("ABC")]
    [InlineData("AB12345")]
    [InlineData("123ABCD")]
    [InlineData("ABC12D3")]
    [InlineData("ABCD123")]
    public void PlacaVeiculo_NaoDevePermitirFormatoInvalido(string placa)
    {
        Assert.Throws<ExcecaoDominio>(() => new PlacaVeiculo(placa));
    }

    [Fact]
    public void Cancelar_DeveAlterarSituacaoParaCancelada()
    {
        var apolice = CriarApolice();

        apolice.Cancelar();

        Assert.Equal(SituacaoApolice.Cancelada, apolice.Situacao);
        Assert.NotNull(apolice.DataAtualizacao);
    }

    [Fact]
    public void VenceNosProximos30Dias_DeveRetornarVerdadeiroParaApoliceAtivaNoPeriodo()
    {
        var apolice = CriarApolice(dataFim: new DateOnly(2026, 7, 20));

        var resultado = apolice.VenceNosProximos30Dias(new DateOnly(2026, 7, 1));

        Assert.True(resultado);
    }

    private static ApoliceSeguro CriarApolice(DateOnly? dataFim = null)
    {
        return ApoliceSeguro.Criar(
            NumeroApolice.Gerar(2026, 1),
            new Documento("123.456.789-01"),
            new PlacaVeiculo("abc1d23"),
            new Dinheiro(199.90m),
            new DateOnly(2026, 7, 1),
            dataFim ?? new DateOnly(2026, 8, 1));
    }
}
