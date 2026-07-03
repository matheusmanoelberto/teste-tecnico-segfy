namespace SegfyInsurance.Application.Abstracoes;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}
