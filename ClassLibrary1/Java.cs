using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Variables;

namespace Lang
{
    public class Java   :ILang
    {
        private StringWriter _output;

        public void CompileAndRun(StringWriter output, string code, IEnumerable<string> namespaces, params Variable[] variables)
        {
            _output = output;
            if (IsCode(code))
            {
                code = new GenerateCodeJava().Generate(
                    code.Replace("[\\]", @"\ssss").Replace("\n", @"\K").Replace("\0", @"\L").Replace(@"0x01", "9773").Replace("\r",@"\i"),
                    namespaces, variables);
                var returntext = JavaExecuter.Execute(code);
                var s = "";

                s = Regex.Unescape(returntext);

                var t = s.Replace(@"\L", "\0").Replace(@"\K", "\n").Replace(@"\ssss", "[\\]").Replace(@"\i","\r");
                t = t.Replace("\a\b\t\n\v\f\r", "\a\b\t\n\v\f\r");
                // res = Encoding.UTF32.GetString(t).Replace(@"\L", @"\0").Replace(@"\K", @"\n").Replace(@"\ssss",@"[\\]");
                var i = new StringBuilder(t).ToString();
                output.Write(t);
            }
            else output.Write(code);
          //  else _output.Write(code);
            
            
           // _output.Write(res.ToString().ToArray());
           // _output.Flush();
        }
        private static bool IsCode(String code)
        {
            return code.Contains("[%") && code.Contains("%]");
        }
    }
}