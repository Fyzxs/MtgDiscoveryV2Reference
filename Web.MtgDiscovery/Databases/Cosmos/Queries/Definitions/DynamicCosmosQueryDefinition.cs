using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.Definitions;

public abstract class DynamicCosmosQueryDefinition : StaticCosmosQueryDefinition
{
    private readonly List<KeyValuePair<string, object>> _parameters = new();

    protected void WithParameters(QueryDefinition queryDefinition)
    {
        foreach(KeyValuePair<string, object> kvp in _parameters)
        {
            queryDefinition.WithParameter(kvp.Key, kvp.Value);
        }
    }

    public DynamicCosmosQueryDefinition WithParameter(string name, object value)
    {
        _parameters.Add(new KeyValuePair<string, object>(name, value));
        return this;
    }
}
