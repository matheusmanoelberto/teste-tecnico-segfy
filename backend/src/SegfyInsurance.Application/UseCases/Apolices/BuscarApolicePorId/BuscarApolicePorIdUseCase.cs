using SegfyInsurance.Application.Abstracoes;

namespace SegfyInsurance.Application.UseCases.Apolices.BuscarApolicePorId;

public class BuscarApolicePorIdUseCase(IApoliceSeguroRepository repositorio)
{
    public async Task<BuscarApolicePorIdResponse?> ExecutarAsync(Guid id, CancellationToken cancellationToken)
    {
        var apolice = await repositorio.BuscarPorIdAsync(id, cancellationToken);

        return apolice is null ? null : BuscarApolicePorIdResponse.APartirDe(apolice);
    }
}

