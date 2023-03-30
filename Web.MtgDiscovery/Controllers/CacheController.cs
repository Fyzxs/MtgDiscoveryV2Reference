using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web.MtgDiscovery.Caching;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CacheController : ControllerBase
{
    private static readonly IMemoryCache s_memoryCache = new MonoStateMemoryCache();

    [HttpGet("Buster")]
    public IActionResult CacheBuster(string key)
    {
        ClearCacheItem(key);

        return NoContent();
    }
    public static void ClearCacheItem(string key) => s_memoryCache.Remove(key);
}
