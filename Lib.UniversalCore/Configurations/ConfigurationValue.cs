using System.Collections.Generic;
using Lib.UniversalCore.Primitives;

namespace Lib.UniversalCore.Configurations;

public abstract class StringConfigurationValue : ToSystemType<string>
{
    private readonly IConfig _config;
    private readonly ConfigurationKey _key;

    protected StringConfigurationValue(ConfigurationKey key) : this(new MonoStateConfig(), key) { }

    private StringConfigurationValue(IConfig config, ConfigurationKey key)
    {
        _config = config;
        _key = key;
    }
    public override string AsSystemType() => _config[_key];
}
internal sealed class InternalStringConfigurationValue : StringConfigurationValue
{
    public InternalStringConfigurationValue(ConfigurationKey key) : base(key) { }
}

public abstract class CollectionConfigurationValue<T> : ToSystemType<ICollection<T>>
{
    private readonly StringConfigurationValue _value;

    protected CollectionConfigurationValue(ConfigurationKey key) : this(new InternalStringConfigurationValue(key)) { }

    private CollectionConfigurationValue(StringConfigurationValue value) => _value = value;

    protected string SourceValue() => _value;
    public bool HasValues() => string.IsNullOrEmpty(_value) is false;
    public bool DoesContain(T value) => AsSystemType().Contains(value);
    public bool DoesNotContain(T value) => !DoesContain(value);
    public abstract override ICollection<T> AsSystemType();
}


public abstract class BoolConfigurationValue : ToSystemType<bool>
{
    private readonly StringConfigurationValue _value;

    protected BoolConfigurationValue(ConfigurationKey key) : this(new InternalStringConfigurationValue(key)) { }

    private BoolConfigurationValue(StringConfigurationValue value) => _value = value;
    public override bool AsSystemType() => bool.TryParse(_value, out bool result) && result;

    public BoolOp IsTrue() => AsSystemType();
    public BoolOp IsFalse() => !IsTrue();
}
