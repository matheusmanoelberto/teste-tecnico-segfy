using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Application.UseCases.Apolices.RemoverApolice;

public class RemoverApoliceUseCase(IApoliceSeguroRepository repositorio)
{
    public Task ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        return repositorio.RemoverAsync(id, cancellationToken);
    }
}

