using eTranslate.Interfaces;

namespace eTranslate
{
    public class eTranslate4CSharp : IeTranslate
    {
        const string _version = "1.0";
        private string CurrentLanguage { get; set; } = "en-US";
        public eTranslate4CSharp(string TranslationFile, string _CurrentLanguage = "en-US")
        {
            if (string.IsNullOrEmpty(TranslationFile))
                throw new Exception("It's not possible ...");
            CurrentLanguage = _CurrentLanguage;
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
            return this;
        }

        public string Translate(string Key, params string[] ParamValues)
        {
            var Texto = Key;
            if(ParamValues != null && ParamValues.Length > 0)
            {
                Texto += " => " + string.Format("{0} de {1}", ParamValues);
            }

            return Texto;
        }
    }
}
