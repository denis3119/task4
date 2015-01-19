
using System;
using System.IO;
using Lang.ServiceReference1;

namespace Lang
{
    public static class JavaExecuter
    {
        public static string Execute(string program)
        {
          //  if (output == null) throw new ArgumentNullException("output");
            var returfromnprogramm = new HelloWorldClient().CompileRun(program);
            //output.Write(returfromnprogramm);
            return returfromnprogramm;
        }
    }
}