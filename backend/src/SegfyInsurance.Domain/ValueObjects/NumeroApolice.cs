using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Domain.ValueObjects;

public sealed record NumeroApolice
{
    public string Valor { get; }

    public NumeroApolice(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ExcecaoDominio("Numero da apolice e obrigatorio.");
        }

        Valor = valor;
    }

    public static NumeroApolice Gerar(int ano, int sequencia)
    {
        if (sequencia <= 0)
        {
            throw new ExcecaoDominio("Sequencia da apolice deve ser maior que zero.");
        }

        return new NumeroApolice($"SEG-{ano}-{sequencia:0000}");
    }

    public override string ToString() => Valor;
}
