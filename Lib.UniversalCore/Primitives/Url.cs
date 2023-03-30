using System;
using Newtonsoft.Json;

namespace Lib.UniversalCore.Primitives;

[JsonConverter(typeof(UrlConverter))]
public abstract class Url : ToSystemType<Uri>
{
    protected string StripStuff(string value) => value.Replace(" ", "").Replace("'", "").Replace(".", "").Replace("/", "").Replace("-", "").Replace(":", "").Replace("'", "");
}

public sealed class UrlConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(Url);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            return new StringUrl((string)reader.Value);
        }

        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        throw new InvalidOperationException("Unhandled case for UrlConverter. Check to see if this converter has been applied to the wrong serialization type.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (null == value)
        {
            writer.WriteNull();
            return;
        }

        if (value is Url uri)
        {
            writer.WriteValue(uri.ToString());
            return;
        }

        throw new InvalidOperationException("Unhandled case for UrlConverter. Check to see if this converter has been applied to the wrong serialization type.");
    }
}
