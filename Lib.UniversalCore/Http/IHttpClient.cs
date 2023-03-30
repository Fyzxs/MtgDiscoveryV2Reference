using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.UniversalCore.Http;

public interface IHttpClient
{
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    Task<T> ResponseAs<T>(Uri uri, CancellationToken token = default);
    Task SaveAsFileAsync(string url, string fileName);
    Task SaveAsFileAsync(Uri uri, string fileName);
}
