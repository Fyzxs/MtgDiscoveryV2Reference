namespace Lib.UniversalCore.Primitives;

public abstract class BoolOp : ToSystemType<bool>
{
    public static bool operator true(BoolOp origin) => origin.AsSystemType();
    public static bool operator false(BoolOp origin) => origin.AsSystemType();
    public static implicit operator BoolOp(bool value) => value ? True : False;

    public static readonly BoolOp True = new StaticTrueBoolOp();
    public static readonly BoolOp False = new StaticFalseBoolOp();

    public BoolOp Not() => new NotOp(this);

    private sealed class StaticTrueBoolOp : BoolOp
    {
        public override bool AsSystemType() => true;
    }
    private sealed class StaticFalseBoolOp : BoolOp
    {
        public override bool AsSystemType() => false;
    }

}
