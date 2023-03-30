namespace Lib.UniversalCore.Primitives;

public sealed class FromBool : BoolOp
{
    private readonly bool _origin;
    public FromBool(bool origin) => _origin = origin;
    public override bool AsSystemType() => _origin;
}
