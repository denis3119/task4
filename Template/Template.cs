using System;
using System.IO;
using Lang;
using Variables;

namespace TemplateCode
{
    public class Template : IDisposable
    {
      //  private String _code;
        private readonly StringWriter _stringWriter=new StringWriter();

        public Template(ILang lang,String codeortext, String[] namespaces,params Variable[] variables)
        {
       
                lang.CompileAndRun(_stringWriter,codeortext,namespaces,variables);
            
           
        }
        public void Render(StringWriter output)
        {
            if (output == null) throw new ArgumentNullException("output");
            output.Write(_stringWriter.ToString());
        }

        public void Dispose()
        {
           
            // throw new NotImplementedException();
           
        }
    }
}
