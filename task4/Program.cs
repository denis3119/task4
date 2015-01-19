
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Lang;
using TemplateCode;
using Variables;

namespace task4
{

    internal static class Program
    {
        private static string UTF8toUnicode(string str)
        {

            byte[] bytUTF8;

            byte[] bytUnicode;

            string strUnicode = String.Empty;



            bytUTF8 = Encoding.UTF8.GetBytes(str);

            bytUnicode = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, bytUTF8);

            strUnicode = Encoding.Unicode.GetString(bytUnicode);

            return strUnicode;

        }
        private static void Main(String[] args)
        {
                    
         //   var t33 = Convert.ToChar("[ddd");
            //var variables = new List<Variable> {new Variable() {name = "t", type =VariableType.Int32},new Variable(){name = "tt",type = VariableType.Int64}};
            var namespaces = new List<String>() {"System", "Lang", "System.Linq"};
            // var t = new GenerateCodeCsharp().Generate(null,variables, namespaces);  */
            /*  var template = new Template(new CSharp(), "[%for(int i = 0; i < 5; i++){%][%=i%][%}%]",
                new []{"System"},new Variable[]{new Variable(){Name = "ttt",Type = VariableType.Int32,Value = 123}, });   */
            /*var template = new Template(new CSharp(), "[% fo%][%r(int i = 0; i < 5; i++){ %]*[%}%]",
            new[] { "System" }, new Variable[] { new Variable() { Name = "ttt", Type = VariableType.Int32, Value = 123 }, */
           /* var template = new Template(new Java(), "[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new String[0], 
                    new Variable("n", VariableType.Int32,2),
                    new Variable("m", VariableType.Int32,1));
            var str = new StringWriter();
            template.Render(str);
            //Console.WriteLine(DateTime.Now);
            var s = new DateTime();      

           // Console.WriteLine(str);
           // var ttt = new HelloWorldClient();
          //var t=  ttt.CompileRun("");
           // Console.WriteLine(t);
            new HelloWorldClient().CompileRun("");
          *//*  var template = new Template(new CSharp(), "[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new String[0],
                  new Variable("n", ArgymentType.Int32, 2),
                  new Variable("m", ArgymentType.Int32, 1));
            var str = new StringWriter();
            template.Render(str);
              */
       /*     var t = "";
            var buffer = new StringBuilder();
            for (int i = 0; i <= 100; i++) buffer.Append((char)i);
            var text = String.Join("", Enumerable.Repeat(buffer.ToString(), 16));

           // t = "";
            foreach (var VARIABLE in text)
            {
                t += (int) VARIABLE+"/";
            }
            //var tt = char.ConvertToUtf32('\n','0');
           // Console.WriteLine(char.MaxValue);
            ///////////////
          //  byte[] asciiString = Encoding.ASCII.GetBytes(text);
           // var b = Encoding.ASCII.GetString(asciiString);
        /*    var newmass = new StringBuilder().Append("");
            foreach (var b1 in asciiString)
            {
                newmass.AppendFormat("[{0}]", b1);
            }
           */// newmass.Append("");
          //  newmass=new StringBuilder().AppendFormat(@"{{ {0} }}",newmass);

            //var y = new UTF8Encoding(Convert.ToBoolean(text));
       //     Console.WriteLine("\u00C4\uD802\u0033\u00AE");
        /*   using (var template = new Template(new CSharp(), text , new String[0]))
           using (var output = new StringWriter())
           {
               template.Render(output);
               //      Assert.AreEqual(text, output.ToString());
               Console.WriteLine(output);
           }    */
            // Console.WriteLine(output.);

           /* var buffer = new StringBuilder();
            for (var i = 0; i < 1000; i++) buffer.Append((char)i);
            var text = String.Join("", Enumerable.Repeat(buffer.ToString(), 16));
            using (var template = new Template(new CSharp(), "[%=2+2%]" + text, new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
               // Assert.AreEqual("4" + text, output.ToString());
                Console.WriteLine(output.ToString());
            }    */
            var t = "\u0000";
            for (int i = 0; i < 100; i++)
            {
               // Console.WriteLine("\u0000"+i.ToString());
            }
            var ty = "\uD800";
            Console.WriteLine("\uD800");
            Console.ReadKey();
        }
     

    }
}
