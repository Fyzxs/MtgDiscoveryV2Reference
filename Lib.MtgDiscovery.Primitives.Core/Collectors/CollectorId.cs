using Lib.UniversalCore.Primitives;

namespace Lib.MtgDiscovery.Primitives.Core.Collectors;

public abstract class CollectorId : ToSystemType<string>
{
    public static implicit operator CollectorId(string origin) => new InternalStringCollectorId(origin);

    private sealed class InternalStringCollectorId : CollectorId
    {
        private readonly string _origin;

        public InternalStringCollectorId(string origin) => _origin = origin;
        public override string AsSystemType() => _origin;
    }

    public bool IsValid() => Guid.TryParse(AsSystemType(), out _);
    public bool IsNotValid() => IsValid() is false;
}
