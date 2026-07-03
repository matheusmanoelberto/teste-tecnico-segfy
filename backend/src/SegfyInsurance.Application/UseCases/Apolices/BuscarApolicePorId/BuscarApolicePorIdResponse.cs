using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Application.UseCases.Apolices.BuscarApolicePorId;

public sealed record BuscarApolicePorIdResponse(
    Guid Id,
    string NumeroApolice,
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim,
    string Situacao)
{
    public static BuscarApolicePorIdResponse APartirDe(ApoliceSeguro apolice)
    {
        return new BuscarApolicePorIdResponse(
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
