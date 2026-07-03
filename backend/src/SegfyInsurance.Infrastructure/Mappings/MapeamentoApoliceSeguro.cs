using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SegfyInsurance.Domain.Entities;
using SegfyInsurance.Domain.Enums;
using SegfyInsurance.Domain.ValueObjects;

namespace SegfyInsurance.Infrastructure.Mappings;

public class MapeamentoApoliceSeguro : IEntityTypeConfiguration<ApoliceSeguro>
{
    public void Configure(EntityTypeBuilder<ApoliceSeguro> construtor)
    {
        construtor.ToTable("InsurancePolicies");
        construtor.HasKey(apolice => apolice.Id);

        construtor.Property(apolice => apolice.Id).HasColumnName("Id").ValueGeneratedNever();
        construtor.Property(apolice => apolice.NumeroApolice)
            .HasColumnName("PolicyNumber")
            .HasConversion(numero => numero.Valor, valor => new NumeroApolice(valor))
            .IsRequired();
        construtor.HasIndex(apolice => apolice.NumeroApolice).IsUnique();

        construtor.Property(apolice => apolice.DocumentoSegurado)
            .HasColumnName("InsuredDocument")
            .HasConversion(documento => documento.Valor, valor => new Documento(valor))
            .IsRequired();

        construtor.Property(apolice => apolice.PlacaVeiculo)
            .HasColumnName("VehiclePlate")
            .HasConversion(placa => placa.Valor, valor => new PlacaVeiculo(valor))
            .IsRequired();

        construtor.Property(apolice => apolice.PremioMensal)
            .HasColumnName("MonthlyPremium")
            .HasColumnType("REAL")
            .HasConversion(premio => premio.Valor, valor => new Dinheiro(valor))
            .IsRequired();

        construtor.Property(apolice => apolice.DataInicio).HasColumnName("StartDate").IsRequired();
        construtor.Property(apolice => apolice.DataFim).HasColumnName("EndDate").IsRequired();
        construtor.Property(apolice => apolice.Situacao).HasColumnName("Status").HasConversion<int>().IsRequired();
        construtor.Property(apolice => apolice.DataCriacao).HasColumnName("CreatedAt").IsRequired();
        construtor.Property(apolice => apolice.DataAtualizacao).HasColumnName("UpdatedAt");
    }
}
