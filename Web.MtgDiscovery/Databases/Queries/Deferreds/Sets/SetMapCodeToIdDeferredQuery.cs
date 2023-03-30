using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Sets;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Sets;

public sealed class SetMapCodeToIdDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public SetMapCodeToIdDeferredQuery(SetCode setCode) : base(new SetMapCodeToIdDatabaseQuery(setCode)) { }
}
