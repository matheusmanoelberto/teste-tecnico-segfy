using Microsoft.Data.Sqlite;

namespace SegfyInsurance.Infrastructure.Database;

public class FabricaConexaoSqlite(string stringConexao)
{
    public SqliteConnection CriarConexao()
    {
        return new SqliteConnection(stringConexao);
    }
}
