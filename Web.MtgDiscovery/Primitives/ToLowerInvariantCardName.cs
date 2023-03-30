namespace Web.MtgDiscovery.Primitives;

public sealed class ToLowerInvariantCardName : CardName
{
    private readonly string _origin;

    public ToLowerInvariantCardName(string origin) => _origin = origin;

    public override string AsSystemType() => _origin.ToLowerInvariant();
}
