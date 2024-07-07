using System.Text.Json;

namespace eTranslate
{
    public static class JsonExtensions
    {
        public static JsonElement? Key(this JsonDocument document, string key) =>
            document.RootElement.Key(key);

        public static JsonElement? Key(this JsonElement element, string key) =>
            element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined &&
            element.TryGetProperty(key, out var value) ? value : (JsonElement?)null;

        public static string? Value(this JsonElement element, string key) =>
            element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined &&
            element.TryGetProperty(key, out var value) ? value.GetString() : (string?)null;
    }
}
