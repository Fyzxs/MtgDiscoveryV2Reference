namespace Lib.MtgDiscovery.Primitives.Core.Collectors;

public sealed class StringCollectorId : CollectorId
{

    private readonly string _origin;

    public StringCollectorId(string origin) => _origin = origin;
    public override string AsSystemType() => _origin;
}
