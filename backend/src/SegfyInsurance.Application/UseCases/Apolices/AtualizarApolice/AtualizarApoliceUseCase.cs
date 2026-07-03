using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Domain.Exceptions;
using SegfyInsurance.Domain.ValueObjects;

namespace SegfyInsurance.Application.UseCases.Apolices.AtualizarApolice;

public class AtualizarApoliceUseCase(IApoliceSeguroRepository repositorio, IUnitOfWork unitOfWork)
{
    public async Task ExecutarAsync(Guid id, AtualizarApoliceRequisicao requisicao, CancellationToken cancellationToken)
    {
        var apolice = await repositorio.BuscarPorIdAsync(id, cancellationToken)
            ?? throw new ExcecaoDominio("Apolice nao encontrada.");

        apolice.Atualizar(
            new Documento(requisicao.DocumentoSegurado),
            new PlacaVeiculo(requisicao.PlacaVeiculo),
            new Dinheiro(requisicao.PremioMensal),
            requisicao.DataInicio,
            requisicao.DataFim);

        await repositorio.AtualizarAsync(apolice, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}

