using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Variables;

namespace Lang
{
    public class CreateRealCode
    {
        private string _codeCode;
        public CreateRealCode(string code)
        {
            this._codeCode =code;
        }

        private static String UnicodeEscaped(Char ch)
        {
            if (ch < 0x10)
            {
                return "\\u000" + CharUnicodeInfo.GetDecimalDigitValue(ch);
            }
            else if (ch < 0x100)
            {
                return "\\u00" + CharUnicodeInfo.GetDecimalDigitValue(ch);
            }
            else if (ch < 0x1000)
            {
                return "\\u0" + CharUnicodeInfo.GetDecimalDigitValue(ch);
            }
            return "\\u" + CharUnicodeInfo.GetDecimalDigitValue(ch);
        }
        public  String ReplaceSpecialSymbols(IEnumerable<string> codetolines, IEnumerable<Variable> variables,String outputfunction,int forcount=0,bool java=false)
        {
            int c = 0;
            var realcode = new StringBuilder("");

            
            var code = new ParseCode(_codeCode).ParseOnlyCode().Count();
            foreach (var item in codetolines)
            {
               // c++;
                String lineofcode;
                string t;

                if (!IsCode("[" + item + "]"))
                {
                    if (java)
                    {

                        var utf16Bytes2 = Encoding.ASCII.GetBytes(item);
                      //  str = "";
                        var unicode = "";
                    /*    foreach (var variable in item)
                        {
                            unicode += UnicodeEscaped(variable);
                        */
                        var str2="";// = utf16Bytes2.Aggregate("", (current, utf16Byte) => current+",");
                        foreach (var VARIABLE in utf16Bytes2)
                        {
                            str2 += VARIABLE + ",";
                        }
                     //   var p = Regex.Escape(item);
                      str2=  str2.Remove(str2.Length - 1);
                        var i2 = forcount++;
                       
                        realcode.AppendFormat("\n String str{1} = \"\";\n byte[] yl{1} = new byte[]{{ {0} }};\n " +
                                              " String y{1} = new String(yl{1}) \n ;" ,
                                              str2, i2);   
                        
                        
                        
                        
                        /* realcode.AppendFormat("String str{1} = \"{0}\";String y{1} = \"\";" +
                                              " for (String ss{1} : str{1}.split(\"-\"))" +
                                              "{{if(ss{1}!=\"\")" +
                                              "y{1}+=new String(Character.toChars(Integer.parseInt(ss{1})));}}", str2, i2);*/
                        realcode.Append(item.Replace(item, outputfunction + "(" + "y" + i2.ToString() + ".toString());")); 
                       // realcode.Append(item.Replace(item, outputfunction + "(\"" + item  + "\");")); 
                        continue;
                    }
                    //new String(bytes, Charset.forName("UTF-8"))
                    //{
                       /* if(VARIABLE.ToString()!="")
                            str += char.ConvertToUtf32(VARIABLE.ToString(),0 ) + '/'.ToString();
                    }  */
                //    var y = "";
                  //  foreach (var s in str.Split('/'))
                    
                     //   if (s!="")
                    //    y += char.ConvertFromUtf32(Convert.ToInt32(s));
                        
                        
                   
                    var utf16Bytes = Encoding.Unicode.GetBytes(item);
                    var temp = utf16Bytes.Select(utf16Byte => new StringBuilder().AppendFormat("{0}/", utf16Byte).ToString());
                    // str = utf16Bytes.Aggregate("", (current, utf16Byte) => current + (utf16Byte + "/"));
                    var i = forcount++;
                 //   str = temp.ToString();
                    var str = temp.Aggregate("", (current, variable) => current + variable);
                   // var str = temp.Aggregate("", (current, s) => current + new StringBuilder().Append(s));
                    realcode.AppendFormat("var l{1} = new List<byte>();var str{1} = \"{0}\";var y{1} = \"\";" +
                                          " foreach (var ss{1} in str{1}.Split('/'))" +
                                          "{{if (ss{1} != \"\") l{1}.Add(byte.Parse(ss{1}));}}" +
                                          "y{1} = Encoding.Unicode.GetString(l{1}.ToArray());", str, i);
                    realcode.Append(item.Replace(item, outputfunction + "(" + "y"+i + ");")); 
                    continue;
                }
                if (item.Contains("%@%"))
                {
                    realcode.Append(item.Replace("%@%", "}"));
                    continue;
                }
                if (item.Contains(@"%@"))
                {
                    realcode.Append(Expression(variables, item + '@', forcount++)); 
                    continue;
                }
                if (item.Contains("%?%"))
                {
                    realcode.Append(item.Replace("%?%", "}")); 
                    continue;
                }
                if (item.Contains("%?"))
                {
                    lineofcode = item.Replace(@"%?", "if(");//.Replace(@"%", "){");
                    lineofcode = lineofcode.Remove(lineofcode.Length - 1); 
                    lineofcode += "){";
                    realcode.Append(lineofcode);
                    continue;
                }

                if (item.Contains("%="))
                {
                    lineofcode = item.Replace(@"%=", outputfunction + "(").Replace(@"%", ");"); 
                    realcode.Append(lineofcode);
                    continue;
                }
               
                if (item.Contains("%"))
                {
                    realcode.Append(item.Replace(@"%", "")); 
                    continue;
                }
              
               
               

            }
            return realcode.ToString();
        }

       
        private  String Expression(IEnumerable<Variable> variables, String code, int forcount)
        {
            var expression = new StringWriter();

            var str = "";
            code = code.Replace("%@", "");
            var fortext = @"for(long for{0}=0;for{0}< {1};for{0}++){{";
            if (Regex.IsMatch(code, @"[\W]"))
            {
                var exp = code.Replace("%@", "");
                str += String.Format(fortext, forcount,exp);
            }
            else
            {
                var variablefromsearch = variables.FirstOrDefault(v => v.Name == code);
                var expr = "";
                if (variablefromsearch != null)
                    expr += variablefromsearch.Value  ;
                else
                    expr += code;
                str += String.Format(fortext, forcount,expr);
            }
            expression.Write(str);
            return expression.ToString();
        }
        public string GenerateNamespaces(IEnumerable<string> librares,string word)
        {
            if (librares == null) throw new ArgumentNullException("librares");
            var namespacestext = new StringBuilder();
            foreach (var ns in librares)
            {
                namespacestext.Append(word+" " + ns + "; ");
            }
            return namespacestext.ToString();
        }

        private static bool IsCode(String code)
        {
            return code.Contains("[%") || code.Contains("%]");
        }
    }
}