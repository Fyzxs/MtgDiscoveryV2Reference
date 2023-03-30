namespace Lib.MtgDiscovery.Primitives.Core.Cards;

public sealed class StringCardId : CardId
{
    private readonly string _origin;

    public StringCardId(string origin) => _origin = origin;

    public override string AsSystemType() => _origin;
}
