namespace Lib.MtgDiscovery.AzureSql.Core.Queries;

public interface IAzureSqlQueryStaticDefinition
{
    string SqlText();
    object SqlParams();
}
