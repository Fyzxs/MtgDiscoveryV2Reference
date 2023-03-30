namespace Web.MtgDiscovery.Primitives;

public sealed class ToLowerInvariantSetCode : SetCode
{
    private readonly string _origin;

    public ToLowerInvariantSetCode(string origin) => _origin = origin;
    public override string AsSystemType() => _origin.ToLowerInvariant();
}
