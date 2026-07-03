using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Application.UseCases.Apolices.ListarApolicesVencendoProximos30Dias;

public sealed record ListarApolicesVencendoProximos30DiasResponse(
    Guid Id,
    string NumeroApolice,
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim,
    string Situacao)
{
    public static ListarApolicesVencendoProximos30DiasResponse APartirDe(ApoliceSeguro apolice)
    {
        return new ListarApolicesVencendoProximos30DiasResponse(
            apolice.Id,
            apolice.NumeroApolice.Valor,
            apolice.DocumentoSegurado.Valor,
            apolice.PlacaVeiculo.Valor,
            apolice.PremioMensal.Valor,
            apolice.DataInicio,
            apolice.DataFim,
            apolice.Situacao.ToString());
    }
}
