namespace Lib.UniversalCore.Primitives;

public abstract class StringEqualityToSystemType<T> : ToSystemType<string> where T : StringEqualityToSystemType<T>
{
    //I'd rather this not use <T>, but don't want to play with GetType and casts right now
    public override int GetHashCode() => ((string)this).GetHashCode();

    public override bool Equals(object obj) => obj is T topic && Equals(topic);
    public bool Equals(T other) => other != null && ((string)this).Equals(((string)other.AsSystemType()));
}
