namespace Web.MtgDiscovery.Caching.Keys;

public sealed class StringCacheKey : CacheKey
{
    private readonly string _key;

    public StringCacheKey(string key) => _key = key;
    public override string AsSystemType() => _key;
}