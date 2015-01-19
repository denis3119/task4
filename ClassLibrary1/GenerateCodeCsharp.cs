using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Variables;

namespace Lang
{
    public class GenerateCodeCsharp
    {
        #region code

        private const String Body = @"
               using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
                     {0}
            namespace CScript
            {{
                public class Script
                {{
                    public static void ScriptMethod()
                    {{
                    {1}      
                     {2}

         
                    }}
                    static void Log(object message)
                    {{
                        if(CharpShell.CharpExecuter.OnExecute != null)
                            CharpShell.CharpExecuter.OnExecute(message);
                    }}
                }}
            }}";

        #endregion

   
        

        private string GenerateVariables( IEnumerable<Variable> variables)
        {
            var variablescode=new StringBuilder();
            foreach (var v in variables)
            {
                if ((v.Type == ArgymentType.String) || (v.Type == ArgymentType.DateTime))
                {
                    var value =new StringBuilder();
                    if (v.Value == null)
                    {
                        value.AppendFormat("= new {0}();",v.Type );
                    }
                    else
                    {
                        value.AppendFormat("=@\"{0}\"; ",v.Value);
                    }

                    variablescode.AppendFormat("global::System.{0} {1}{2}",v.Type,v.Name,value);
                }
                else

                    variablescode.AppendFormat("global::System.{0} {1}={2}; ",v.Type,v.Name,v.Value);
            }
            return variablescode.ToString();
        }
        public String Generate(String code, IEnumerable<String> namespaces,bool text=false,bool java=false, params Variable[] variables)
        {
            string t = null;
            if (text)
            {
                var t1 =new CreateRealCode(code).GenerateNamespaces(namespaces, "using");
                var t2 = GenerateVariables(variables);
                var t3 =new CreateRealCode(code).ReplaceSpecialSymbols(new List<string> {code}, variables, "Log");
                t = new StringBuilder().AppendFormat(Body,t1 , t2, t3 ).ToString();
            }
            else
            { 
                var parse = new ParseCode(code);
                var codeandtextlines = parse.ParseCodeAndText();
                  t = new StringBuilder().AppendFormat(Body, new CreateRealCode(code).GenerateNamespaces(namespaces, "using"),
                GenerateVariables(variables),new CreateRealCode(code).ReplaceSpecialSymbols(codeandtextlines, variables, "Log")).ToString();
            }
            return t;
        }

        
        
    }
}