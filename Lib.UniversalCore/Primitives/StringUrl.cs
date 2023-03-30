using System;

namespace Lib.UniversalCore.Primitives;

public sealed class StringUrl : Url
{
    public static implicit operator StringUrl(string source) => new(source);

    private readonly string _uri;

    public StringUrl(string uri) => _uri = uri;
    public override Uri AsSystemType() => new(_uri);
}
