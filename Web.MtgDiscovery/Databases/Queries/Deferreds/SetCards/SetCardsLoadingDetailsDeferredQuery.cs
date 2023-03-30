using Lib.UniversalCore.Serializes;
using Web.MtgDiscovery.Databases.Cosmos.Queries.SetCards;
using Web.MtgDiscovery.Primitives;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds.SetCards;

public sealed class SetCardsLoadingDetailsDeferredQuery : DeferredQuery<ISelfSerialize>
{
    public SetCardsLoadingDetailsDeferredQuery(SetCode setCode) : base(new SetCardsLoadingDetailsDatabaseQuery(setCode)) { }
}
