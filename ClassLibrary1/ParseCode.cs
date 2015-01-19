using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lang
{
    public class ParseCode
    {
       // Regex _variableRegex = new Regex(@"\[%=\S*?%\]");
        //
        readonly Regex _codeandtext = new Regex(@"((?!\[)(?!\]).)+");  //((?!!\[)(?!!\]).)+ -----  \[+([^\[\]]+)\]+
        /// <summary>
        /// ((?!\[)(?!\]).)+
        /// </summary>
        /// 
        readonly Regex _codeonly =new Regex(@"\[+([^\[\\\]]+)\]+"); //new Regex(@"\[+([^\[\]]+)\]+");
        private readonly String _text;

        public ParseCode(String text)
        {
            _text = text;
          //  _text = _text;
            // _text = _text.Replace("\n", "\\n");
            //  _text = _text.Replace("\0", "\u0000");
        }

        public IEnumerable<String> ParseCodeAndText()
        {
            var result = new List<String>();
            foreach (var variable in _codeandtext.Matches(_text))
            {
                   result.Add(variable.ToString());
            }
            return result;
        }
        public IEnumerable<String> ParseOnlyCode()
        {
            var result = new List<String>();
            foreach (var variable in _codeonly.Matches(_text))
            {    //if ()
                result.Add(variable.ToString());
            }
            return result;
        }
    }
}