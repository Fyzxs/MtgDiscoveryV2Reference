using System;
using System.Threading.Tasks;
using Lib.UniversalCore.Databases.Queries;
using Lib.UniversalCore.Primitives;
using Lib.UniversalCore.Threading;

namespace Web.MtgDiscovery.Databases.Queries.Deferreds;

public abstract class DeferredQuery<T> : ToSystemType<Func<Task<T>>>
{
    private readonly IDatabaseQuery<T> _query;

    protected DeferredQuery(IDatabaseQuery<T> query) => _query = query;

    public override Func<Task<T>> AsSystemType() => async () => await _query.ExecuteAsync().NoSync();
}
