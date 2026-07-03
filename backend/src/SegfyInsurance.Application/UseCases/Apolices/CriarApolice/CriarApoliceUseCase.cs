using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Domain.Entities;
using SegfyInsurance.Domain.ValueObjects;

namespace SegfyInsurance.Application.UseCases.Apolices.CriarApolice;

public class CriarApoliceUseCase(IApoliceSeguroRepository repositorio)
{
    public async Task<CriarApoliceResponse> ExecutarAsync(CriarApoliceRequisicao requisicao, CancellationToken cancellationToken)
    {
        var anoAtual = DateTime.UtcNow.Year;
        var sequencia = await repositorio.BuscarProximaSequenciaPorAnoAsync(anoAtual, cancellationToken);
        var numeroApolice = NumeroApolice.Gerar(anoAtual, sequencia);

        var apolice = ApoliceSeguro.Criar(
            numeroApolice,
            new Documento(requisicao.DocumentoSegurado),
            new PlacaVeiculo(requisicao.PlacaVeiculo),
            new Dinheiro(requisicao.PremioMensal),
            requisicao.DataInicio,
            requisicao.DataFim);

        await repositorio.AdicionarAsync(apolice, cancellationToken);

        return CriarApoliceResponse.APartirDe(apolice);
    }
}

