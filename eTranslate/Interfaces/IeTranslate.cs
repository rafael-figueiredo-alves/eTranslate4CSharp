using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTranslate.Interfaces
{
    public interface IeTranslate
    {
        public string Version();
        public string GetLanguage();
        public IeTranslate SetLanguage(string Language);
        public string Translate(string Key, params string[] ParamValues);
    }
}
