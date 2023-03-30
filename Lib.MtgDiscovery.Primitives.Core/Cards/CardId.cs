using Lib.UniversalCore.Primitives;

namespace Lib.MtgDiscovery.Primitives.Core.Cards;

public abstract class CardId : ToSystemType<string>
{
    public static implicit operator CardId(string origin) => new InternalStringCardId(origin);

    private sealed class InternalStringCardId : CardId
    {
        private readonly string _origin;

        public InternalStringCardId(string origin) => _origin = origin;
        public override string AsSystemType() => _origin;
    }

    public bool IsValid() => Guid.TryParse(AsSystemType(), out _);
    public bool IsNotValid() => IsValid() is false;
}
