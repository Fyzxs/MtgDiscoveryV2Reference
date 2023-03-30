
namespace Lib.MtgDiscovery.AzureSql.Core.Commands.SprocArgs;

public interface IAzSqlSprocArgs<out T>
{
    T AsSqlParams();
}
