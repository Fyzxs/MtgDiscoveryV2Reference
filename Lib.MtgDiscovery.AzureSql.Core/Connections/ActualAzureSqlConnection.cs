using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Lib.UniversalCore.Logging;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Lib.MtgDiscovery.AzureSql.Core.Connections;

public sealed class ActualAzureSqlConnection : IAzureSqlConnection
{
    private static readonly ISimpleLogger s_logger = new SimplisticConsoleSimpleLogger();

    private readonly AzureSqlConnectionString _connectionString;
#if DEBUG
    public static void TestSet(SqlConnection testInstance) => s_testInstance = testInstance;
#endif
    private static SqlConnection s_testInstance;

    public ActualAzureSqlConnection(AzureSqlConnectionString connectionString) => _connectionString = connectionString;

    private async Task<SqlConnection> Connection()
    {
        if (s_testInstance is not null) return s_testInstance;

        SqlConnection sqlConnection = new(_connectionString);
        await sqlConnection.OpenAsync();

        return sqlConnection;
    }

    public async Task UpsertAsync(string sql, object param = default)
    {
        DebugIt(sql, param);
        await using SqlConnection sqlConnection = await Connection();
        await sqlConnection.ExecuteAsync(sql, param);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = default)
    {
        DebugIt(sql, param);
        await Task.Delay(0); //Hack to not use Task wrapping stuff
        return await QueryAsyncEnumerable<T>(sql, param).ToListAsync();
    }

    public async IAsyncEnumerable<T> QueryAsyncEnumerable<T>(string sql, object param = default)
    {
        DebugIt(sql, param);
        await using SqlConnection sqlConnection = await Connection();

        await using DbDataReader reader = await sqlConnection.ExecuteReaderAsync(sql, param);
        Func<IDataReader, T> rowParser = reader.GetRowParser<T>();

        while (await reader.ReadAsync())
            yield return rowParser(reader);
        while (await reader.NextResultAsync()) { }
    }

    [Conditional("DEBUG")]
    private static void DebugIt(string sql, object param)
    {
        string serializeObject = JsonConvert.SerializeObject(param);
        s_logger.LogInformation($"[sql={sql}] [param={serializeObject}]");
        if (serializeObject.Contains("undefined")) { s_logger.LogInformation(new System.Diagnostics.StackTrace().ToString()); }
    }
}
