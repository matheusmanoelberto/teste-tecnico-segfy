using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Domain.ValueObjects;

public sealed record Documento
{
    public string Valor { get; }

    public Documento(string valor)
    {
        var somenteDigitos = new string((valor ?? string.Empty).Where(char.IsDigit).ToArray());

        if (somenteDigitos.Length is not 11 and not 14)
        {
            throw new ExcecaoDominio("Documento deve ser CPF com 11 digitos ou CNPJ com 14 digitos.");
        }

        Valor = somenteDigitos;
    }

    public override string ToString() => Valor;
}
