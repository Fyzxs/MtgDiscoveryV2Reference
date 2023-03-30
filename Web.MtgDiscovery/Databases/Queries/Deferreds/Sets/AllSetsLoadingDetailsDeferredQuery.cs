using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Sets;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Sets;

public sealed class AllSetsLoadingDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public AllSetsLoadingDetailsDeferredQuery() : base(new AllSetsLoadingDetailsDatabaseQuery()) { }
}
