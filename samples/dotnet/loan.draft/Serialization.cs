using System.Text.Json;
using System.Text.Json.Serialization;

namespace loan.draft;

/// <summary>
/// When applied to an integer type field, this field will not be serialized if the value is zero.
/// usage: [JsonConverter(typeof(IgnoreZeroConverter))]
///
/// Note: if this is applied to a field that is not an integer, runtime errors will result. 
/// </summary>
public class IgnoreZeroConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Implement if needed
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        if (value == 0)
            return;
        
        JsonSerializer.Serialize(writer, value, options);
    }
}
