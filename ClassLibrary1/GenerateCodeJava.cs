using System;
using System.Collections.Generic;
using System.Text;
using Variables;

namespace Lang
{
    public class GenerateCodeJava
    {
        private string body = "package test;{0} public class Test {{ public Test(){{ " +
                              
                              "    {1}   }}}}";
      

        private string GenerateVariables(Variable[] variables)
        {
            var variablescode = new StringBuilder();
            foreach (var v in variables)
            {
                if ((v.Type == ArgymentType.String) || (v.Type == ArgymentType.Calendar))
                {
                    var value = "";
                    if (v.Value == null)
                    {
                        var str = "new " + v.Type + "();";
                        if (v.Type == ArgymentType.Calendar) str = "java.util.Calendar.getInstance();";
                        value = "=  " + str;
                    }
                    else
                    {
                        value = "=\"" + v.Value + "\"; ";
                    }

                    variablescode.Append(TypeReplace(v.Type) + " " + v.Name + value);
                }
                else
                {
                    if ((v.Type == ArgymentType.Int64) || (v.Type == ArgymentType.Long))
                        v.Value = "Long.valueOf(" + v.Value + ")";
                    variablescode.Append(TypeReplace(v.Type) + " " + v.Name + "=" + v.Value + "; ");
                }
            }
            return variablescode.ToString();
        }

        private string TypeReplace(ArgymentType type)
        {
            var typejava = "java.lang.";
           
            switch (type)
            {
                case ArgymentType.Int32: typejava += "Integer";break;
                case ArgymentType.Int64: typejava += "Long"; break;
                case ArgymentType.String: typejava += "String"; break;
                case ArgymentType.Long: typejava += "Long"; break;
                case ArgymentType.Double: typejava += "Double"; break;
                case ArgymentType.DateTime: typejava = "java.util.Calendar"; break;
                case ArgymentType.Calendar: typejava = "java.util.Calendar"; break;
                case ArgymentType.Boolean: typejava += "Boolean"; break;
            }
            return typejava;
        }
        public String Generate(String code, IEnumerable<String> namespaces, params Variable[] variables)
        {
            var format = new CreateRealCode(code);
            var result = new StringBuilder();
           var import =(new CreateRealCode(code).GenerateNamespaces(namespaces,"import"));
            var parse = new ParseCode(code);

            var codeandtext = parse.ParseCodeAndText();
            var onlycode = parse.ParseOnlyCode();
            result.Append(GenerateVariables(variables));
            result.Append(new CreateRealCode(code).ReplaceSpecialSymbols(codeandtext, variables, "System.out.print",java:true) /*+ footer*/);
            var check = new StringBuilder().AppendFormat(body, import, result).ToString();
            return check;
        }

        private int _forcount;
       


    }
}