namespace SegfyInsurance.Application.UseCases.Apolices.AtualizarApolice;

public sealed record AtualizarApoliceRequisicao(
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim);
