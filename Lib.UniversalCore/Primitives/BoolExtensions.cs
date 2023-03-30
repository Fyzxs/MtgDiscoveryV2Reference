namespace Lib.UniversalCore.Primitives;

public static class BoolExtensions
{
    public static bool Not(this bool origin) => !origin;
    public static bool Or(this bool origin, bool other) => origin || other;
}