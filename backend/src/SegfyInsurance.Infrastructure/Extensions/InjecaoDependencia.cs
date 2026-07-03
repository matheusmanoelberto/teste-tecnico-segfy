using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Infrastructure.Data;
using SegfyInsurance.Infrastructure.Database;
using SegfyInsurance.Infrastructure.Repositories;

namespace SegfyInsurance.Infrastructure.Extensions;

public static class InjecaoDependencia
{
    public static IServiceCollection AdicionarInfraestrutura(this IServiceCollection servicos, IConfiguration configuracao)
    {
        var stringConexao = configuracao.GetConnectionString("BancoSegfy")
            ?? "Data Source=segfy-insurance.db";

        servicos.AddDbContext<SegfyInsuranceDbContext>(opcoes => opcoes.UseSqlite(stringConexao));
        servicos.AddSingleton(new FabricaConexaoSqlite(stringConexao));
        servicos.AddScoped<IApoliceSeguroRepository, ApoliceSeguroRepository>();

        return servicos;
    }
}

