namespace eTranslate.Interfaces
{
    public interface IeTranslate
    {
        /// <summary>
        /// Add events to call when a new language is set
        /// </summary>
        public void AddEventToRunOnSetLanguage(Action _event);

        /// <summary>
        /// Get the version of the library
        /// </summary>
        /// <returns>The version of the library</returns>
        public string Version();

        /// <summary>
        /// Get the current language in use
        /// </summary>
        /// <returns>The current language in use</returns>
        public string GetLanguage();

        /// <summary>
        /// Sets a new language for the component and fires the event associated with OnSetLanguage
        /// </summary>
        /// <param name="Language">A string containing the language in the following pattern: pt-BR, en-US</param>
        /// <returns>Access to the interface methods</returns>
        public IeTranslate SetLanguage(string Language);

        /// <summary>
        /// Main method to translate the strings to current language
        /// </summary>
        /// <param name="Key">The key you want to get from Translation file</param>
        /// <param name="ParamValues">Aditional values to be used in a string like: {1} of {2} to be filled with the provided values</param>
        /// <returns>The string from translation file filled with the additional parameters if they are provided</returns>
        public Task<string> Translate(string Key, string DefaultValue = "", params string[] ParamValues);
    }
}
