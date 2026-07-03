using Microsoft.Data.Sqlite;
using SegfyInsurance.Application.Abstracoes;
using SegfyInsurance.Domain.Entities;
using SegfyInsurance.Domain.Enums;
using SegfyInsurance.Domain.ValueObjects;
using SegfyInsurance.Infrastructure.Database;

namespace SegfyInsurance.Infrastructure.Repositories;

public class ApoliceSeguroRepository(FabricaConexaoSqlite fabricaConexao) : IApoliceSeguroRepository
{
    public async Task AdicionarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken)
    {
        const string sql = """
            INSERT INTO InsurancePolicies
                (Id, PolicyNumber, InsuredDocument, VehiclePlate, MonthlyPremium, StartDate, EndDate, Status, CreatedAt, UpdatedAt)
            VALUES
                (@Id, @PolicyNumber, @InsuredDocument, @VehiclePlate, @MonthlyPremium, @StartDate, @EndDate, @Status, @CreatedAt, @UpdatedAt);
            """;

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        AdicionarParametrosApolice(comando, apolice);
        await comando.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task AtualizarAsync(ApoliceSeguro apolice, CancellationToken cancellationToken)
    {
        const string sql = """
            UPDATE InsurancePolicies
            SET InsuredDocument = @InsuredDocument,
                VehiclePlate = @VehiclePlate,
                MonthlyPremium = @MonthlyPremium,
                StartDate = @StartDate,
                EndDate = @EndDate,
                Status = @Status,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;
            """;

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        AdicionarParametrosApolice(comando, apolice);
        await comando.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "DELETE FROM InsurancePolicies WHERE Id = @Id;";

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        comando.Parameters.AddWithValue("@Id", id.ToString());
        await comando.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task<ApoliceSeguro?> BuscarPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM InsurancePolicies WHERE Id = @Id;";

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        comando.Parameters.AddWithValue("@Id", id.ToString());
        await using var leitor = await comando.ExecuteReaderAsync(cancellationToken);

        return await leitor.ReadAsync(cancellationToken) ? MapearApolice(leitor) : null;
    }

    public async Task<IReadOnlyList<ApoliceSeguro>> ListarTodosAsync(CancellationToken cancellationToken)
    {
        const string sql = "SELECT * FROM InsurancePolicies ORDER BY CreatedAt DESC;";

        return await ListarAsync(sql, cancellationToken);
    }

    public async Task<IReadOnlyList<ApoliceSeguro>> ListarVencendoProximos30DiasAsync(CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT *
            FROM InsurancePolicies
            WHERE EndDate BETWEEN date('now') AND date('now', '+30 days')
              AND Status = 1
            ORDER BY EndDate;
            """;

        return await ListarAsync(sql, cancellationToken);
    }

    public async Task<int> BuscarProximaSequenciaPorAnoAsync(int ano, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT COUNT(*)
            FROM InsurancePolicies
            WHERE PolicyNumber LIKE @Prefixo;
            """;

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        comando.Parameters.AddWithValue("@Prefixo", $"SEG-{ano}-%");
        var quantidade = Convert.ToInt32(await comando.ExecuteScalarAsync(cancellationToken));

        return quantidade + 1;
    }

    private async Task<IReadOnlyList<ApoliceSeguro>> ListarAsync(string sql, CancellationToken cancellationToken)
    {
        var apolices = new List<ApoliceSeguro>();

        await using var conexao = fabricaConexao.CriarConexao();
        await conexao.OpenAsync(cancellationToken);
        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;
        await using var leitor = await comando.ExecuteReaderAsync(cancellationToken);

        while (await leitor.ReadAsync(cancellationToken))
        {
            apolices.Add(MapearApolice(leitor));
        }

        return apolices;
    }

    private static void AdicionarParametrosApolice(SqliteCommand comando, ApoliceSeguro apolice)
    {
        comando.Parameters.AddWithValue("@Id", apolice.Id.ToString());
        comando.Parameters.AddWithValue("@PolicyNumber", apolice.NumeroApolice.Valor);
        comando.Parameters.AddWithValue("@InsuredDocument", apolice.DocumentoSegurado.Valor);
        comando.Parameters.AddWithValue("@VehiclePlate", apolice.PlacaVeiculo.Valor);
        comando.Parameters.AddWithValue("@MonthlyPremium", apolice.PremioMensal.Valor);
        comando.Parameters.AddWithValue("@StartDate", apolice.DataInicio.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("@EndDate", apolice.DataFim.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("@Status", (int)apolice.Situacao);
        comando.Parameters.AddWithValue("@CreatedAt", apolice.DataCriacao.ToString("O"));
        comando.Parameters.AddWithValue("@UpdatedAt", apolice.DataAtualizacao?.ToString("O") ?? (object)DBNull.Value);
    }

    private static ApoliceSeguro MapearApolice(SqliteDataReader leitor)
    {
        return ApoliceSeguro.Restaurar(
            Guid.Parse(leitor.GetString(leitor.GetOrdinal("Id"))),
            new NumeroApolice(leitor.GetString(leitor.GetOrdinal("PolicyNumber"))),
            new Documento(leitor.GetString(leitor.GetOrdinal("InsuredDocument"))),
            new PlacaVeiculo(leitor.GetString(leitor.GetOrdinal("VehiclePlate"))),
            new Dinheiro(leitor.GetDecimal(leitor.GetOrdinal("MonthlyPremium"))),
            DateOnly.Parse(leitor.GetString(leitor.GetOrdinal("StartDate"))),
            DateOnly.Parse(leitor.GetString(leitor.GetOrdinal("EndDate"))),
            (SituacaoApolice)leitor.GetInt32(leitor.GetOrdinal("Status")),
            DateTime.Parse(leitor.GetString(leitor.GetOrdinal("CreatedAt"))),
            leitor.IsDBNull(leitor.GetOrdinal("UpdatedAt")) ? null : DateTime.Parse(leitor.GetString(leitor.GetOrdinal("UpdatedAt"))));
    }
}

