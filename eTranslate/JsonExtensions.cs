using System.Text.Json;

namespace eTranslate
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Extension method to interate through a JSON telling the key and getting its value;
        /// </summary>
        /// <param name="document">JsonDocument</param>
        /// <param name="key">A setring with the key</param>
        /// <returns>JsonElement</returns>
        public static JsonElement? Key(this JsonDocument document, string key) =>
            document.RootElement.Key(key);

        /// <summary>
        /// Extension method to interate through a JSON telling the key and getting its value;
        /// </summary>
        /// <param name="document">JsonDocument</param>
        /// <param name="key">A setring with the key</param>
        /// <returns>JsonElement</returns>
        public static JsonElement? Key(this JsonElement element, string key) =>
            element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined &&
            element.TryGetProperty(key, out var value) ? value : (JsonElement?)null;

        /// <summary>
        /// Extension method to interate through a JSON telling the key of a string value you want to get;
        /// </summary>
        /// <param name="element">JsonElement</param>
        /// <param name="key">A setring with the key</param>
        /// <returns>JsonElement</returns>
        public static string? Value(this JsonElement element, string key) =>
            element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined &&
            element.TryGetProperty(key, out var value) ? value.GetString() : (string?)null;
    }
}
