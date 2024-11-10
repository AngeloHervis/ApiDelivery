using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crosscutting.Extensions;

public static class JsonExtensions
{
    private static readonly JsonSerializerOptions WriteSerializerOptions = new()
    {
        WriteIndented = false,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    public static string Serializar<T>(this T value)
    {
        return JsonSerializer.Serialize(value, WriteSerializerOptions);
    }

    public static List<string> GetJsonPropertyNames<T>(string propertyIgnore = "")
    {
        var type = typeof(T);
        var properties = type.GetProperties();

        var jsonPropertyNames = new List<string>();

        foreach (var property in properties)
        {
            var jsonAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonAttribute != null && (propertyIgnore == "" || jsonAttribute.Name != propertyIgnore))
            {
                jsonPropertyNames.Add(jsonAttribute.Name);
            }
        }

        return jsonPropertyNames;
    }
}