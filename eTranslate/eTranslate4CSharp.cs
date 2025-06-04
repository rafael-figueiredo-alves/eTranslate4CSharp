using eTranslate.Interfaces;
using System.Text.Json;

namespace eTranslate
{
    public class eTranslate4CSharp : IeTranslate
    {
        const string _version = "1.2.0";

        #region Variables
        private string CurrentLanguage { get; set; } = "en-US";
        private JsonDocument? TranslationJSON { get; set; } = null;
        private HttpClient? httpClient { get; set; } = null;
        private string translationFile = string.Empty;
        #endregion

        public eTranslate4CSharp(string TranslationFile, string _CurrentLanguage = "en-US", HttpClient? _httpClient = null)
        {
            if (string.IsNullOrEmpty(TranslationFile))
                throw new Exception("It's not possible to continue without setting a Translation File!");

            httpClient = _httpClient;

            CurrentLanguage = _CurrentLanguage;
            translationFile = TranslationFile;
        }

        #region Private methods
        private async Task<JsonElement?> Language()
        {
            if (TranslationJSON == null)
            {
                string JsonContent = string.Empty;

                if (httpClient != null)
                {
                    if (Uri.IsWellFormedUriString(translationFile, UriKind.Absolute))
                    {
                        JsonContent = await httpClient.GetStringAsync(translationFile);
                    }
                    else
                    {
                        JsonContent = System.IO.File.ReadAllText(translationFile);
                    }
                }
                else
                    JsonContent = System.IO.File.ReadAllText(translationFile);

                TranslationJSON = JsonDocument.Parse(JsonContent);
            }

            return TranslationJSON.Key(CurrentLanguage);
        }

        private string[] GetAllKeysFromKey(string key)
        {
            return key.Split('.');
        }

        private async Task<string?> GetValueFromKey(string key)
        {
            JsonElement? elemento = await Language();
            string? Retorno = null;
            var keys = GetAllKeysFromKey(key);

            for (int index = 0; index < keys.Length; index++)
            {
                if (index != keys.Length - 1)
                {
                    elemento = elemento?.Key(keys[index]);
                }
                else
                {
                    if (elemento.HasValue && elemento.Value.TryGetProperty(keys[index], out JsonElement finalValue))
                    {
                        if (finalValue.ValueKind == JsonValueKind.String)
                        {
                            Retorno = finalValue.GetString();
                        }
                        // Opcional: Tratar outros casos, como objetos, números, etc.
                        else
                        {
                            Retorno = null;
                        }
                    }
                }
            }

            return Retorno;
        }

        private string? FillValueWithValues(string? value, params string[] ParamValues)
        {
            if (value != null)
                return string.Format(value, ParamValues);
            else
                throw new Exception("Value was not found for the key informed. Please check the key and correct it.");
        }
        #endregion

        #region Interface Methods
        private List<WeakReference<Action>>? _OnSetLanguage { get; set; } = new();

        /// <summary>
        /// Add events to call when a new language is set
        /// </summary>
        public void AddEventToRunOnSetLanguage(Action _event)
        {
            if(_OnSetLanguage == null)
                _OnSetLanguage = new();

            _OnSetLanguage.Add(new WeakReference<Action>(_event));
        }

        private void OnSetLanguage()
        {
            if( _OnSetLanguage != null)
                foreach (var weakRef in _OnSetLanguage.ToArray()) // Clonar a lista para evitar modificações durante a iteração
                {
                    if (weakRef.TryGetTarget(out var handler))
                    {
                        handler.Invoke();
                    }
                    else
                    {
                        _OnSetLanguage.Remove(weakRef); // Remove referências inválidas
                    }
                }
        }

        /// <summary>
        /// Get the version of the library
        /// </summary>
        /// <returns>The version of the library</returns>
        public string Version()
        {
            return _version;
        }

        /// <summary>
        /// Get the current language in use
        /// </summary>
        /// <returns>The current language in use</returns>
        public string GetLanguage()
        {
            return CurrentLanguage;
        }

        /// <summary>
        /// Sets a new language for the component and fires the event associated with OnSetLanguage
        /// </summary>
        /// <param name="Language">A string containing the language in the following pattern: pt-BR, en-US</param>
        /// <returns>Access to the interface methods</returns>
        public IeTranslate SetLanguage(string Language)
        {
            this.CurrentLanguage = Language;
            OnSetLanguage();
            return this;
        }

        /// <summary>
        /// Main method to translate the strings to current language
        /// </summary>
        /// <param name="Key">The key you want to get from Translation file</param>
        /// <param name="ParamValues">Aditional values to be used in a string like: {1} of {2} to be filled with the provided values</param>
        /// <returns>The string from translation file filled with the additional parameters if they are provided</returns>
        public async Task<string> Translate(string Key, string DefaultValue = "", params string[] ParamValues)
        {
            string? valueFromKey = await GetValueFromKey(Key);

            if (ParamValues != null && ParamValues.Length > 0 && !string.IsNullOrEmpty(valueFromKey))
            {
                return FillValueWithValues(valueFromKey, ParamValues) ?? DefaultValue;
            }
            else
                return valueFromKey ?? DefaultValue;
        }
        #endregion
    }
}
