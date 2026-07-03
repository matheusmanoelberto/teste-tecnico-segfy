using Microsoft.Extensions.DependencyInjection;
using SegfyInsurance.Application.UseCases.Apolices.AtualizarApolice;
using SegfyInsurance.Application.UseCases.Apolices.BuscarApolicePorId;
using SegfyInsurance.Application.UseCases.Apolices.CancelarApolice;
using SegfyInsurance.Application.UseCases.Apolices.CriarApolice;
using SegfyInsurance.Application.UseCases.Apolices.ListarApolices;
using SegfyInsurance.Application.UseCases.Apolices.ListarApolicesVencendoProximos30Dias;
using SegfyInsurance.Application.UseCases.Apolices.RemoverApolice;

namespace SegfyInsurance.Application.Extensions;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarAplicacao(this IServiceCollection servicos)
    {
        servicos.AddScoped<CriarApoliceUseCase>();
        servicos.AddScoped<BuscarApolicePorIdUseCase>();
        servicos.AddScoped<ListarApolicesUseCase>();
        servicos.AddScoped<AtualizarApoliceUseCase>();
        servicos.AddScoped<CancelarApoliceUseCase>();
        servicos.AddScoped<RemoverApoliceUseCase>();
        servicos.AddScoped<ListarApolicesVencendoProximos30DiasUseCase>();

        return servicos;
    }
}
