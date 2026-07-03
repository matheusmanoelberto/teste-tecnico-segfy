using System.Text.RegularExpressions;
using SegfyInsurance.Domain.Exceptions;

namespace SegfyInsurance.Domain.ValueObjects;

public enum TipoPlaca
{
    Antiga,
    Mercosul
}

public sealed record PlacaVeiculo
{
    private static readonly Regex PlacaAntigaRegex = new(@"^[A-Z]{3}[0-9]{4}$", RegexOptions.Compiled);
    private static readonly Regex PlacaMercosulRegex = new(@"^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$", RegexOptions.Compiled);

    public string Valor { get; }

    public bool EhMercosul => PlacaMercosulRegex.IsMatch(Valor);

    public TipoPlaca Tipo => EhMercosul ? TipoPlaca.Mercosul : TipoPlaca.Antiga;

    public PlacaVeiculo(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ExcecaoDominio("Placa do veiculo e obrigatoria.");
        }

        var placa = valor
            .Trim()
            .Replace("-", string.Empty)
            .Replace(" ", string.Empty)
            .ToUpperInvariant();

        if (placa.Length != 7)
        {
            throw new ExcecaoDominio("A placa deve possuir exatamente 7 caracteres.");
        }

        if (!PlacaAntigaRegex.IsMatch(placa) && !PlacaMercosulRegex.IsMatch(placa))
        {
            throw new ExcecaoDominio("Placa invalida. Utilize o padrao ABC1234 ou ABC1D23 (Mercosul).");
        }

        Valor = placa;
    }

    public override string ToString() => Valor;
}
