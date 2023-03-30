using Lib.UniversalCore.Primitives;

namespace Lib.MtgDiscovery.Primitives.Core.Sets;

public abstract class SetId : ToSystemType<string>
{
    public static implicit operator SetId(string origin) => new InternalStringSetId(origin);

    private sealed class InternalStringSetId : SetId
    {
        private readonly string _origin;

        public InternalStringSetId(string origin) => _origin = origin;
        public override string AsSystemType() => _origin;
    }
}
