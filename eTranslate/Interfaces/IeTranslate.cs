namespace eTranslate.Interfaces
{
    public interface IeTranslate
    {
        public event Action? Evento;
        public string Version();
        public string GetLanguage();
        public IeTranslate SetLanguage(string Language);
        public Task<string> Translate(string Key, params string[] ParamValues);
    }
}
