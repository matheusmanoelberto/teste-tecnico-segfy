using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Application.UseCases.Apolices.CancelarApolice;

public class CancelarApoliceUseCase(IApoliceSeguroRepository repositorio)
{
    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        var apolice = await repositorio.BuscarPorIdAsync(id, cancellationToken)
            ?? throw new ExcecaoDominio("Apolice nao encontrada.");

        apolice.Cancelar();
        await repositorio.AtualizarAsync(apolice, cancellationToken);
    }
}

