namespace Lib.UniversalCore.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhitespace(this string value) => string.IsNullOrWhiteSpace(value);
    public static bool IsNotNullOrWhitespace(this string value) => string.IsNullOrWhiteSpace(value) is false;
}
