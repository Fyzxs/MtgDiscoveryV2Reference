using System.Collections.Generic;
using System.Linq;

namespace Lib.UniversalCore.Extensions;

public static class EnumerableExtensions
{
    public static bool Empty<T>(this IEnumerable<T> value) => value.Any() is false;
}
