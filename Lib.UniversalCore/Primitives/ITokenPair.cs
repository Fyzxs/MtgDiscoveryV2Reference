namespace Lib.UniversalCore.Primitives;

public interface ITokenPair
{
    public ToSystemType<string> Key();
    public ToSystemType<string> Value();

}