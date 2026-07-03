namespace SegfyInsurance.Application.UseCases.Apolices.CriarApolice;

public sealed record CriarApoliceRequisicao(
    string DocumentoSegurado,
    string PlacaVeiculo,
    decimal PremioMensal,
    DateOnly DataInicio,
    DateOnly DataFim);
