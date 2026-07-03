using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Application.UseCases.Apolices.CriarApolice;

public sealed record CriarApoliceResponse(
    Guid Id,
    string NumeroApolice,
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim,
    string Situacao,
    DateTime DataCriacao,
    DateTime? DataAtualizacao)
{
    public static CriarApoliceResponse APartirDe(ApoliceSeguro apolice)
    {
        return new CriarApoliceResponse(
            apolice.Id,
            apolice.NumeroApolice.Valor,
            apolice.DocumentoSegurado.Valor,
            apolice.PlacaVeiculo.Valor,
            apolice.PremioMensal.Valor,
            apolice.DataInicio,
            apolice.DataFim,
            apolice.Situacao.ToString(),
            apolice.DataCriacao,
            apolice.DataAtualizacao);
    }
}
