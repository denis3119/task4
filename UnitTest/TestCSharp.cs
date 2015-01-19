using System;
using System.IO;
using System.Linq;
using System.Text;
using Lang;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemplateCode;
using Variables;

namespace UnitTest
{
    [TestClass]
    public class TestCSharp
    {
        [TestMethod]
        public void Text()   /////0
        {
            using (var template = new Template(new CSharp(), "abcd", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("abcd", output.ToString());
            }
        }

        [TestMethod]
        public void TwoCycles()       //1
        {
            using (var template = new Template(new CSharp(), @"[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new String[0],
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("m", ArgymentType.Int32, 1),
                new Variable("n", ArgymentType.Int32, 2)))
            using (var output = new StringWriter())
            {
                // String.e
                template.Render(output);
                Assert.AreEqual("<***><***>", output.ToString());
            }
        }
        [TestMethod]
        public void CycleForAndText()       //2
        {
            using (var template = new Template(new CSharp(), "[% fo%][%r(int i = 0; i < 5; i++){ %]*[%}%]", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("*****", output.ToString());
            }
        }

        [TestMethod]
        public void CycleForAndWriteI()  //3
        {
            using (var template = new Template(new CSharp(), "[%for(int i = 0; i < 5; i++){%][%=i%][%}%]", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("01234", output.ToString());
            }
        }

        [TestMethod]
        public void BoolSimple()    //4
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%]TRUE[%@3%]![%@%][%?%]", new String[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }
        
        [TestMethod]
        public void VerificationCode()  //5
        {
            var buffer = new StringBuilder();
            for (var i = Char.MinValue; i <Char.MaxValue; i++) buffer.Append((char)i);
            var text = String.Join("", Enumerable.Repeat(buffer.ToString(), 16));
            using (var template = new Template(new CSharp(), "[%=4%]" + text + "[%=2*3*4%]", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("4"+text + "24", output.ToString());
            }
        }
         
        [TestMethod]
        public void TwoСycles()
        {
            using (var template = new Template(new CSharp(), "[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new String[0], 
                    new Variable("n", ArgymentType.Int32,2),
                    new Variable("m", ArgymentType.Int32,1)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("<***><***>", output.ToString());
            } 
        }

        

        [TestMethod]
        public void CodeCycleFor()
        {
            using (var template = new Template(new CSharp(), "[%for(int i =0;i<10;i++) Log(i);%]", new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("0123456789", output.ToString());
            }
        }
         [TestMethod]
        public void CodeForAndBoolType()
        {
            using (var template = new Template(new CSharp(), @"[%for(int i =0;i<10;i++) %][%?s.Equals(""TEST"")%]TRUE[%@3%]![%@%][%?%][%;%]", new String[0],
                    new Variable() { Name = "s", Type = ArgymentType.String, Value = "TEST" }))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!TRUE!!!", output.ToString());
            }
        }
         

        
        
        [TestMethod]
        public void TextSimple()
        {
            using (var template = new Template(new CSharp(),"****",new String[0] ))
            using (var output=new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("****",output.ToString());
            }
           
        }
        [TestMethod]
        public void BoolTypeCycleFor()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%for(int i = 0; i < 5; i++){%][%=i%][%}%][%?%]", new String[0],new Variable("s",ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("01234", output.ToString());
            }

        }
        [TestMethod]
        public void Test3()
        {
           

            using (var template = new Template(new CSharp(), "****5",new String[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("****5", output.ToString());
            }
        }

        [TestMethod]
        public void BoolTypeCycleForFrom0To10050()
        {
            var text = "";
            for (int i = 0; i < 10050; i++)
            {
                text += i;
            }
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%for(int i = 0; i < 10050; i++){%][%=i%][%}%][%?%]", new String[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual(text, output.ToString());
            }

        }
        [TestMethod]
        public void TwoСyclesBig()
        {
            const int n = 2; const int m = 4; 
            var text = "";
            for (var for0 = 0; for0 < 2; for0++)
            {
                text+=("<");
                for (var for1 = 0; for1 < n % m + n + m + n * Math.Pow(100, 2); for1++)
                {
                    text+=("*");
                }
                text+=(">");
            } 
     
            using (var template = new Template(new CSharp(), "[%@n%]<[%@ n % m + n+m +n*Math.Pow(100,2) %]*[%@%]>[%@%]", new[] { "System" },
                new Variable("n", ArgymentType.Int32, n),
                new Variable("m", ArgymentType.Int32, m)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual(text, output.ToString());
            }
        }
        [TestMethod]
        public void DateTimeAndBoolType()
        {
            var date = new DateTime();
            using (var template = new Template(new CSharp(), @"[%=s.Year%]", new[] { "System" }, new Variable("s", ArgymentType.DateTime)))
            using (var output = new StringWriter())
            {
                template.Render(output);
               
                Assert.AreEqual(date.Year.ToString(), output.ToString());
            }

        }
        [TestMethod]
        public void DateTimeAndCycleFor()
        {
          
            using (var template = new Template(new CSharp(), @"[% fo%][%r(int i = 0; i < 5; i++){ %][%=s.Year%][%}%]", new[] { "System" }, new Variable("s", ArgymentType.DateTime)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual("11111", output.ToString());
            }
        }

        [TestMethod]
        public void Int32AndInt64()
        {
            
            using (var template = new Template(new CSharp(), @"[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new[] { "System" }, 
                new Variable("n", ArgymentType.Int32,2),
                new Variable("m",ArgymentType.Int64,1)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual("<***><***>", output.ToString());
            }

        }
        [TestMethod]
        public void Int64AndInt64()
        {
            var t = "";
            const int n = 7;
            const long m = 3;
            for (var for0 = 0; for0 < 7; for0++)
            {
                t += ("<");
                for (var for1 = 0; for1 < n + m; for1++)
                {
                    t += ("*");

                } t += (">");
            }
            using (var template = new Template(new CSharp(), @"[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new[] { "System" },
                new Variable("n", ArgymentType.Int64, n),
                new Variable("m", ArgymentType.Int64, m)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual(t, output.ToString());
            }

        }
       // [%for(int i =0;i<10;i++)%][%@n%]<[%@ n + m %]*[%@%]>[%@%][%;%]
      
        [TestMethod]
        public void CodeCycleForx3()
        {
            var t = "";
            const int n = 3;
            const long m = 7;
            for (int i = 0; i < 10; i++)
            for (var for0 = 0; for0 < n; for0++)
            {
                t += ("<");
                for (var for1 = 0; for1 < n + m; for1++)
                {
                    t += ("*");

                } t += (">");
            }
            using (var template = new Template(new CSharp(), @"[%for(int i =0;i<10;i++)%][%@3%]<[%@ 3 + 7 %]*[%@%]>[%@%][%;%]", new[] { "System" }))
            using (var output = new StringWriter())
            {                                                     
                template.Render(output);
                Assert.AreEqual(t, output.ToString());
            }
        }
        
        [TestMethod]
        public void BoolSimpleAndVariable()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%=i%][%@3%]![%@%][%?%]", new String[0], 
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("i",ArgymentType.Int32,1)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("1!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimpleAndVariablesMath()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%=Math.Pow(i,i)%][%@3%]![%@%][%?%]", new[] { "System" },
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("i", ArgymentType.Int32, 2)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("4!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimpleAndVariables()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%=i+i%][%@3%]![%@%][%?%]", new String[0],
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("i", ArgymentType.Int32, 1)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("2!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolTypeCycleForMath()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TEST"")%][%for(int i = 0; i < Math.Pow(3,2); i++){%][%=i%][%}%][%?%]", new []{"System"},
                new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("012345678", output.ToString());
            }

        }
        [TestMethod]
        public void BoolSimpleLongText()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(""TESTTESTTEST%TESTTESTTESTTESTTESTTESTTEST"")%]TRUE[%@3%]![%@%][%?%]", new String[0], 
                new Variable("s", ArgymentType.String, "TESTTESTTEST%TESTTESTTESTTESTTESTTESTTEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolAndChar()
        {
            using (var template = new Template(new CSharp(), @"[%?s.Equals(s.ToString())%]TRUE[%@3%]![%@%][%?%]9", new String[0],
                new Variable("s", ArgymentType.String, "TESTTESTTEST%TESTTESTTESTTESTTESTTESTTEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!9", output.ToString());
            }
        }
        [TestMethod]
        public void _3_35nightAdd_4_25()
        {
            using (var template = new Template(new CSharp(), @"[%@i%][%? i < e%][%=i+i%][%break;%][%?%][%@%]", new String[0],
                new Variable("i", ArgymentType.Int32, 4), new Variable("e",ArgymentType.Int32,6)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("8", output.ToString());
            }
        }
    }
}
