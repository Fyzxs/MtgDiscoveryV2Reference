using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.Sets;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.Sets;

public sealed class SetDisplayDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public SetDisplayDetailsDeferredQuery(SetCode setCode) : base(new SetDisplayDetailsDatabaseQuery(setCode)) { }
}
