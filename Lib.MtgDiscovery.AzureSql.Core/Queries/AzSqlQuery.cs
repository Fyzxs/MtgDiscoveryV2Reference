using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.MtgDiscovery.AzureSql.Core.Connections;

namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public abstract class AzSqlQuery<ReturnType> : IAzureSqlQueryAsync<ReturnType>
{
    private readonly IAzureSqlConnection _sqlConnection;

    protected AzSqlQuery(AzureSqlConnectionString stringConfigurationValue) : this(new ActualAzureSqlConnection(stringConfigurationValue)) { }

    protected AzSqlQuery(IAzureSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

    public async Task<IEnumerable<ReturnType>> ExecuteAsync(IAzureSqlQueryStaticDefinition query) => await _sqlConnection.QueryAsync<ReturnType>(query.SqlText(), query.SqlParams());
}

public abstract class AzSqlUpsert : IAzureSqlUpsertAsync
{
    private readonly IAzureSqlConnection _sqlConnection;

    protected AzSqlUpsert(AzureSqlConnectionString stringConfigurationValue) : this(new ActualAzureSqlConnection(stringConfigurationValue)) { }

    protected AzSqlUpsert(IAzureSqlConnection sqlConnection) => _sqlConnection = sqlConnection;

    public async Task ExecuteAsync(IAzureSqlQueryStaticDefinition query) => await _sqlConnection.UpsertAsync(query.SqlText(), query.SqlParams());
}
