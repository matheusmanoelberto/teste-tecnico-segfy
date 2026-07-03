using SegfyInsurance.Domain.Common.Base;
using SegfyInsurance.Domain.Enums;
using SegfyInsurance.Domain.Exceptions;
using SegfyInsurance.Domain.ValueObjects;

namespace SegfyInsurance.Domain.Entities;

public class ApoliceSeguro : Entity
{
    private ApoliceSeguro()
    {
    }

    private ApoliceSeguro(
        NumeroApolice numeroApolice,
        Documento documentoSegurado,
        PlacaVeiculo placaVeiculo,
        Dinheiro premioMensal,
        DateOnly dataInicio,
        DateOnly dataFim)
    {
        ValidarVigencia(dataInicio, dataFim);

        NumeroApolice = numeroApolice;
        DocumentoSegurado = documentoSegurado;
        PlacaVeiculo = placaVeiculo;
        PremioMensal = premioMensal;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Situacao = SituacaoApolice.Ativa;
    }

    private ApoliceSeguro(
        Guid id,
        NumeroApolice numeroApolice,
        Documento documentoSegurado,
        PlacaVeiculo placaVeiculo,
        Dinheiro premioMensal,
        DateOnly dataInicio,
        DateOnly dataFim,
        SituacaoApolice situacao,
        DateTime dataCriacao,
        DateTime? dataAtualizacao = null)
        : base(id, dataCriacao, dataAtualizacao)
    {
        ValidarVigencia(dataInicio, dataFim);

        NumeroApolice = numeroApolice;
        DocumentoSegurado = documentoSegurado;
        PlacaVeiculo = placaVeiculo;
        PremioMensal = premioMensal;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Situacao = situacao;
    }

    public NumeroApolice NumeroApolice { get; private set; } = null!;
    public Documento DocumentoSegurado { get; private set; } = null!;
    public PlacaVeiculo PlacaVeiculo { get; private set; } = null!;
    public Dinheiro PremioMensal { get; private set; } = null!;
    public DateOnly DataInicio { get; private set; }
    public DateOnly DataFim { get; private set; }
    public SituacaoApolice Situacao { get; private set; }

    public static ApoliceSeguro Criar(
        NumeroApolice numeroApolice,
        Documento documentoSegurado,
        PlacaVeiculo placaVeiculo,
        Dinheiro premioMensal,
        DateOnly dataInicio,
        DateOnly dataFim)
    {
        return new ApoliceSeguro(numeroApolice, documentoSegurado, placaVeiculo, premioMensal, dataInicio, dataFim);
    }

    public static ApoliceSeguro Restaurar(
        Guid id,
        NumeroApolice numeroApolice,
        Documento documentoSegurado,
        PlacaVeiculo placaVeiculo,
        Dinheiro premioMensal,
        DateOnly dataInicio,
        DateOnly dataFim,
        SituacaoApolice situacao,
        DateTime dataCriacao,
        DateTime? dataAtualizacao = null)
    {
        return new ApoliceSeguro(id, numeroApolice, documentoSegurado, placaVeiculo, premioMensal, dataInicio, dataFim, situacao, dataCriacao, dataAtualizacao);
    }

    public void Atualizar(Documento documentoSegurado, PlacaVeiculo placaVeiculo, Dinheiro premioMensal, DateOnly dataInicio, DateOnly dataFim)
    {
        ValidarVigencia(dataInicio, dataFim);

        DocumentoSegurado = documentoSegurado;
        PlacaVeiculo = placaVeiculo;
        PremioMensal = premioMensal;
        DataInicio = dataInicio;
        DataFim = dataFim;
        MarcarComoAtualizado();
    }

    public void Cancelar()
    {
        Situacao = SituacaoApolice.Cancelada;
        MarcarComoAtualizado();
    }

    public void Expirar()
    {
        Situacao = SituacaoApolice.Expirada;
        MarcarComoAtualizado();
    }

    public bool VenceNosProximos30Dias(DateOnly hoje)
    {
        return Situacao == SituacaoApolice.Ativa && DataFim >= hoje && DataFim <= hoje.AddDays(30);
    }

    private static void ValidarVigencia(DateOnly dataInicio, DateOnly dataFim)
    {
        if (dataFim < dataInicio)
        {
            throw new ExcecaoDominio("Data final da vigencia deve ser maior ou igual a data inicial.");
        }
    }
}
