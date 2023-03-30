using System;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Lib.UniversalCore.Threading;
using Newtonsoft.Json;

namespace Lib.UniversalCore.Http;

public sealed class MonoStateHttpClient : IHttpClient
{
#if DEBUG
    public static void TestSet(HttpClient testInstance) => s_httpClient = testInstance;
#endif
    private static HttpClient s_httpClient;

    private HttpClient MonoState() => s_httpClient ??= new HttpClient();
    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) => await MonoState().SendAsync(request).NoSync();
    public async Task<T> ResponseAs<T>(Uri uri, CancellationToken token)
    {
        HttpClient client = MonoState();
        Stream s = await client.GetStreamAsync(uri, token).NoSync();
        await using ConfiguredAsyncDisposable _ = s.NoSync();
        using StreamReader sr = new StreamReader(s);
        await using JsonReader reader = new JsonTextReader(sr);
        JsonSerializer serializer = new JsonSerializer();
        return serializer.Deserialize<T>(reader);
    }

    public async Task SaveAsFileAsync(string url, string fileName)
    {
        Stream stream = await MonoState().GetStreamAsync(url).NoSync();
        await using ConfiguredAsyncDisposable _ = stream.NoSync();
        await using FileStream fileStream = new(fileName, FileMode.Create);
        await stream.CopyToAsync(fileStream).NoSync();
    }

    public async Task SaveAsFileAsync(Uri uri, string fileName)
    {
        Stream stream = await MonoState().GetStreamAsync(uri).NoSync();
        await using ConfiguredAsyncDisposable _ = stream.NoSync();
        await using FileStream fileStream = new(fileName, FileMode.Create);
        await stream.CopyToAsync(fileStream).NoSync();
    }

    public async Task<Stream> StreamAsync(string url) => await MonoState().GetStreamAsync(url).NoSync();
}
