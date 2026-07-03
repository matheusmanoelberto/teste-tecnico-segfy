using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Application.Abstracoes;

public interface IApoliceSeguroRepository
{
    Task AdicionarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken);
    Task AtualizarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken);
    Task RemoverAsync(Guid id, CancellationToken cancellationToken);
    Task<ApoliceSeguro?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ApoliceSeguro>> ListarTodosAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<ApoliceSeguro>> ListarVencendoProximos30DiasAsync(CancellationToken cancellationToken);
    Task<int> BuscarProximaSequenciaPorAnoAsync(int ano, CancellationToken cancellationToken);
}

