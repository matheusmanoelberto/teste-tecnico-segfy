using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Application.UseCases.Apolices.ListarApolicesVencendoProximos30Dias;

public class ListarApolicesVencendoProximos30DiasUseCase(IApoliceSeguroRepository repositorio)
{
    public async Task<IReadOnlyList<ListarApolicesVencendoProximos30DiasResponse>> ExecutarAsync(CancellationToken cancellationToken)
    {
        var apolices = await repositorio.ListarVencendoProximos30DiasAsync(cancellationToken);

        return apolices.Select(ListarApolicesVencendoProximos30DiasResponse.APartirDe).ToList();
    }
}

