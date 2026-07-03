using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Application.UseCases.Apolices.ListarApolices;

public class ListarApolicesUseCase(IApoliceSeguroRepository repositorio)
{
    public async Task<IReadOnlyList<ListarApolicesResponse>> ExecutarAsync(CancellationToken cancellationToken)
    {
        var apolices = await repositorio.ListarTodosAsync(cancellationToken);

        return apolices.Select(ListarApolicesResponse.APartirDe).ToList();
    }
}

