using System;
using System.Collections.Generic;
using System.IO;
using CharpShell;
using Variables;

namespace Lang
{
    public class CSharp :ILang
    {
        private void Log(object msg)
        {
            _output.Write(String.Concat(msg).Replace(@"\L", "\0").Replace(@"\K", "\n").Replace(@"\ssss", "[\\]"));
        }

        private StringWriter _output;
        public void CompileAndRun(StringWriter output, string code, IEnumerable<string> namespaces, params Variable[] variables)
        {
            _output = output;
                code = new GenerateCodeCsharp().Generate(
                    code.Replace("[\\]", @"\ssss").Replace("\n", @"\K").Replace("\0", @"\L").Replace(@"0x01", "9773"),
                    namespaces, variables:variables);
                new CharpExecuter(Log).Execute(code);
           
        }
    }

}