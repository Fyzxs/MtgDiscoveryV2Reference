using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Lib.UniversalCore.Threading;

public static class TaskExtensions
{
    public static ConfiguredTaskAwaitable NoSync(this Task t) => t.ConfigureAwait(false);
    public static ConfiguredTaskAwaitable<T> NoSync<T>(this Task<T> t) => t.ConfigureAwait(false);
    public static ConfiguredValueTaskAwaitable NoSync(this ValueTask vt) => vt.ConfigureAwait(false);
    public static ConfiguredValueTaskAwaitable<T> NoSync<T>(this ValueTask<T> vt) => vt.ConfigureAwait(false);
    public static ConfiguredAsyncDisposable NoSync(this IAsyncDisposable vt) => vt.ConfigureAwait(false);
    public static ConfiguredCancelableAsyncEnumerable<T> NoSync<T>(this IAsyncEnumerable<T> vt) => vt.ConfigureAwait(false);
    public static ConfiguredCancelableAsyncEnumerable<T> NoSync<T>(this ConfiguredCancelableAsyncEnumerable<T> vt) => vt.ConfigureAwait(false);
    public static void Sync(this Task t) => t.GetAwaiter().GetResult();
    public static T Sync<T>(this Task<T> t) => t.GetAwaiter().GetResult();
    public static void Sync(this ValueTask vt) => vt.GetAwaiter().GetResult();
    public static T Sync<T>(this ValueTask<T> vt) => vt.GetAwaiter().GetResult();
}
