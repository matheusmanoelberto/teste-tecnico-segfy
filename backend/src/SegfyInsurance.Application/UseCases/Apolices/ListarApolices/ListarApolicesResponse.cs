using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Application.UseCases.Apolices.ListarApolices;

public sealed record ListarApolicesResponse(
    Guid Id,
    string NumeroApolice,
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim,
    string Situacao)
{
    public static ListarApolicesResponse APartirDe(ApoliceSeguro apolice)
    {
        return new ListarApolicesResponse(
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
