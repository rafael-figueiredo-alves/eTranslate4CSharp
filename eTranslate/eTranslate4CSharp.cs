using eTranslate.Interfaces;

namespace eTranslate
{
    public class eTranslate4CSharp : IeTranslate
    {
        const string _version = "1.0";
        private string Text { get; set; } = "Teste";
        public eTranslate4CSharp() { }
        public string Version()
        {
            return _version;
        }

        public string GetText()
        {
            return Text;
        }

        public IeTranslate SetText(string text)
        {
            this.Text = text;
            return this;
        }
    }
}
