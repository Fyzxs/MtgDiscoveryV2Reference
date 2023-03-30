﻿using Lib.MtgDiscovery.AzureSql.Core.Queries;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;

namespace Web.MtgDiscovery.Databases.AzureSql.Queries.Definitions.Collectors;

public sealed class CollectorSetCardCountDetailsAzureSqlQueryDefinition : IAzureSqlQueryStaticDefinition
{
    private readonly SetId _setId;
    private readonly CollectorId _collectorId;

    public CollectorSetCardCountDetailsAzureSqlQueryDefinition(SetId setId, CollectorId collectorId)
    {
        _setId = setId;
        _collectorId = collectorId;
    }

    public string SqlText() => "SELECT * FROM CollectorSetCardsCount WHERE CollectorId = @CollectorId and SetId = @SetId";

    public object SqlParams() => new { CollectorId = _collectorId.AsSystemType(), SetId = _setId.AsSystemType() };
}