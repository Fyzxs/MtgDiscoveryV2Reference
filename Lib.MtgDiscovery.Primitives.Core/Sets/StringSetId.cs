namespace Lib.MtgDiscovery.Primitives.Core.Sets;

public sealed class StringSetId : SetId
{
    private readonly string _origin;

    public StringSetId(string origin) => _origin = origin;
    public override string AsSystemType() => _origin;
}
