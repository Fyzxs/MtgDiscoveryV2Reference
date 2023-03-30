using System.Diagnostics;
using System.Threading.Tasks;

namespace Lib.UniversalCore.Primitives;

[DebuggerDisplay("[{GetType().Name}]:{AsSystemType()}")]
public abstract class ToSystemType<TSystemType>
{
    public static implicit operator TSystemType(ToSystemType<TSystemType> source) => source.AsSystemType();

    public abstract TSystemType AsSystemType();
    public override string ToString() => $"{AsSystemType()}";
}

[DebuggerDisplay("[{GetType().Name}]:[{AsSystemType()}]")]
public abstract class ToSystemTypeAsync<TSystemType> : ToSystemType<TSystemType>
{
    public static implicit operator TSystemType(ToSystemTypeAsync<TSystemType> origin) => origin.AsSystemType();

    public override TSystemType AsSystemType() => AsSystemTypeAsync().GetAwaiter().GetResult();
    public abstract Task<TSystemType> AsSystemTypeAsync();
}
