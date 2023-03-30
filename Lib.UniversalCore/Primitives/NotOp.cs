namespace Lib.UniversalCore.Primitives;

public sealed class NotOp : BoolOp
{
    private readonly ToSystemType<bool> _origin;

    public NotOp(ToSystemType<bool> origin) => _origin = origin;
    public override bool AsSystemType() => !_origin;
}
