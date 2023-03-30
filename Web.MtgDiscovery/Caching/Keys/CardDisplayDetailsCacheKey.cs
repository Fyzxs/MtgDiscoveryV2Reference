using Lib.MtgDiscovery.Primitives.Core.Cards;

namespace Web.MtgDiscovery.Caching.Keys;

public sealed class CardDisplayDetailsCacheKey : DynamicCacheKey
{
    public CardDisplayDetailsCacheKey(CardId cardId) : base("{0}-card-display-details", cardId) { }
}
