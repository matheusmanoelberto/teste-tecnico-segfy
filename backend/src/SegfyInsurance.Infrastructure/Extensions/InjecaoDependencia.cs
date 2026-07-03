using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Infrastructure.Data;
using SegfyInsurance.Infrastructure.Repositories;

namespace SegfyInsurance.Infrastructure.Extensions;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection servicos, IConfiguration configuracao)
    {
        var stringConexao = configuracao.GetConnectionString("BancoSegfy")
            ?? throw new InvalidOperationException("Connection string 'BancoSegfy' nao foi configurada.");

        servicos.AddDbContext<SegfyInsuranceDbContext>(opcoes => opcoes.UseSqlite(stringConexao));
        servicos.AddScoped<IApoliceSeguroRepository, ApoliceSeguroRepository>();

        return servicos;
    }
}

