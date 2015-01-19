using System.Collections.Generic;
using System.IO;
using Variables;

namespace Lang
{
    public interface ILang
    {
        void CompileAndRun(StringWriter output, string code, IEnumerable<string> namespaces, params Variable[] variables);
       
    }
}
