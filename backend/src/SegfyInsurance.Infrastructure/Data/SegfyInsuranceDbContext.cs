using Microsoft.EntityFrameworkCore;
using SegfyInsurance.Domain.Entities;

namespace SegfyInsurance.Infrastructure.Data;

public class SegfyInsuranceDbContext(DbContextOptions<SegfyInsuranceDbContext> opcoes) : DbContext(opcoes)
{
    public DbSet<ApoliceSeguro> ApolicesSeguro => Set<ApoliceSeguro>();

    protected override void OnModelCreating(ModelBuilder construtorModelo)
    {
        construtorModelo.ApplyConfiguration(new Mappings.MapeamentoApoliceSeguro());
    }
}
