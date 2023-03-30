using System.Threading.Tasks;

namespace Lib.UniversalCore.Databases.Queries;

public interface IDatabaseQuery<T>
{
    Task<T> ExecuteAsync();
}
