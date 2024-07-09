using eTranslate.Interfaces;
using System;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace eTranslate
{
    public class eTranslate4CSharp : IeTranslate
    {
        const string _version = "1.0";

        #region Variables
        private string CurrentLanguage { get; set; } = "en-US";
        private JsonDocument? TranslationJSON { get; set; } = null;
        private HttpClient? httpClient { get; set; } = null;
        private string translationFile = string.Empty;
        #endregion

        public eTranslate4CSharp(string TranslationFile, string _CurrentLanguage = "en-US", HttpClient? _httpClient = null)
        {
            if (string.IsNullOrEmpty(TranslationFile))
                throw new Exception("It's not possible ...");

            httpClient = _httpClient;

            CurrentLanguage = _CurrentLanguage;
            translationFile = TranslationFile;
        }


        public event Action? OnSetLanguage;

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

            for(int index = 0; index < keys.Length; index++)
            {
                if(index != keys.Length - 1)
                {
                    elemento = elemento?.Key(keys[index]);
                }
                else
                {
                    Retorno = elemento?.Value(keys[index]);
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

        public string Version()
        {
            return _version;
        }

        public string GetLanguage()
        {
            return CurrentLanguage;
        }

        public IeTranslate SetLanguage(string Language)
        {
            this.CurrentLanguage = Language;
            OnSetLanguage?.Invoke();
            return this;
        }

        public async Task<string> Translate(string Key, params string[] ParamValues)
        {
            string? valueFromKey = await GetValueFromKey(Key);

            if (ParamValues != null && ParamValues.Length > 0)
            {
                return FillValueWithValues(valueFromKey, ParamValues) ?? string.Empty;
            }
            else
                return valueFromKey ?? string.Empty;
        }
    }
}
