using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Domain.ValueObjects;

public sealed record Dinheiro
{
    public decimal Valor { get; }

    public Dinheiro(decimal valor)
    {
        if (valor <= 0)
        {
            throw new ExcecaoDominio("Valor do premio deve ser maior que zero.");
        }

        Valor = valor;
    }

    public override string ToString() => Valor.ToString("F2");
}
