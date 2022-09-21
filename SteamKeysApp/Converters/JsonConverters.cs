using Newtonsoft.Json;

namespace SteamKeysApp.Converters;

public class NullToZeroConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ValueType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return 0.0;
        }

        return reader.Value;
    }

    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}