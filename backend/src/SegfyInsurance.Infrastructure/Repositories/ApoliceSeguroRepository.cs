using Microsoft.EntityFrameworkCore;
using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Domain.Entities;
using SegfyInsurance.Infrastructure.Data;

namespace SegfyInsurance.Infrastructure.Repositories;

public class ApoliceSeguroRepository(SegfyInsuranceDbContext contexto) : IApoliceSeguroRepository
{
    public async Task AdicionarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken)
    {
        await contexto.ApolicesSeguro.AddAsync(apolice, cancellationToken);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken)
    {
        contexto.ApolicesSeguro.Update(apolice);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        var apolice = await contexto.ApolicesSeguro.FindAsync([id], cancellationToken);

        if (apolice is null)
        {
            return;
        }

        contexto.ApolicesSeguro.Remove(apolice);
        await contexto.SaveChangesAsync(cancellationToken);
    }

    public async Task<ApoliceSeguro?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await contexto.ApolicesSeguro
            .FromSqlRaw("""
                SELECT *
                FROM InsurancePolicies
                WHERE Id = {0}
                """, id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ApoliceSeguro>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        return await contexto.ApolicesSeguro
            .FromSqlRaw("""
                SELECT *
                FROM InsurancePolicies
                ORDER BY CreatedAt DESC
                """)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ApoliceSeguro>> ListarVencendoProximos30DiasAsync(CancellationToken cancellationToken)
    {
        return await contexto.ApolicesSeguro
            .FromSqlRaw("""
                SELECT *
                FROM InsurancePolicies
                WHERE EndDate BETWEEN date('now') AND date('now', '+30 days')
                  AND Status = 1
                ORDER BY EndDate
                """)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<int> BuscarProximaSequenciaPorAnoAsync(int ano, CancellationToken cancellationToken)
    {
        var prefixo = $"SEG-{ano}-%";
        var quantidade = await contexto.ApolicesSeguro
            .FromSqlRaw("""
                SELECT *
                FROM InsurancePolicies
                WHERE PolicyNumber LIKE {0}
                """, prefixo)
            .AsNoTracking()
            .CountAsync(cancellationToken);

        return quantidade + 1;
    }
}
