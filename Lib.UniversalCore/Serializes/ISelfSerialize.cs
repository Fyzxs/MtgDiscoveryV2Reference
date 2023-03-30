using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lib.UniversalCore.Serializes;
public interface ISelfSerialize
{
    string Serialize() => JsonConvert.SerializeObject(this);
}

public sealed class CollectionSelfSerialize : ISelfSerialize
{
    private readonly List<ISelfSerialize> _list;

    public CollectionSelfSerialize() : this(new List<ISelfSerialize>()) { }
    public CollectionSelfSerialize(IEnumerable<ISelfSerialize> incoming) : this(new List<ISelfSerialize>(incoming)) { }

    private CollectionSelfSerialize(List<ISelfSerialize> list) => _list = list;
    public void Add(ISelfSerialize item) => _list.Add(item);
    public void AddRange(IEnumerable<ISelfSerialize> collection) => _list.AddRange(collection);

    public string Serialize() => JsonConvert.SerializeObject(_list);
}

public sealed class EmptySelfSerialize : ISelfSerialize
{
    public string Serialize() => string.Empty;
}

public sealed class RawSelfSerialize : ISelfSerialize
{
    private readonly string _origin;

    public RawSelfSerialize(string origin) => _origin = origin;
    public string Serialize() => _origin;
}
