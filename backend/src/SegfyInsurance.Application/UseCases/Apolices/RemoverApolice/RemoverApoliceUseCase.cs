using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Application.UseCases.Apolices.RemoverApolice;

public class RemoverApoliceUseCase(IApoliceSeguroRepository repositorio, IUnitOfWork unitOfWork)
{
    public async Task ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        await repositorio.RemoverAsync(id, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}

