using System;
using System.Diagnostics;
using Lib.UniversalCore.Primitives;

namespace Web.MtgDiscovery.Caching.Keys;

public abstract class DynamicCacheKey : CacheKey
{
    private readonly string _format;
    private readonly ToSystemType<string>[] _tokens;

    protected DynamicCacheKey(string format, params ToSystemType<string>[] tokens)
    {
        _format = format;
        _tokens = tokens;
    }

    public override string AsSystemType()
    {
        try
        {
            // ReSharper disable once CoVariantArrayConversion
            return string.Format(_format, _tokens);
        }
        catch(Exception ex)
        {
            Debugger.Break();
            throw;
        }
    }
}
