using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Infrastructure.Data;

public class UnitOfWork(SegfyInsuranceDbContext contexto) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await contexto.SaveChangesAsync(cancellationToken);
    }
}
