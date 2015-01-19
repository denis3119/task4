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
    public class TestJava
    {
        [TestMethod]
        public void CycleForAndText()         //1
        {
            using (var template = new Template(new Java(), "[%fo%][%r(int i = 0; i < 5; i++){%]*[%}%]", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("*****", output.ToString());
            }
        }
        [TestMethod]
        public void Text()   //0
        {
            using (var template = new Template(new Java(), "abcd", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("abcd", output.ToString());
            }
        }
        [TestMethod]
        public void TwoCycles()                  //3
        {
            using (var template = new Template(new Java(), @"[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new string[0],
               
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
        public void CycleForAndWriteI()    //2
        {
            using (var template = new Template(new Java(), "[%for(int i = 0; i < 5; i++){%][%=i%][%}%]", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("01234", output.ToString());
            }
        }
        [TestMethod]
        public void VerificationCode()    //5
        {
            var buffer = new StringBuilder();
            for (int i = char.MinValue; i <= 100; i++) buffer.Append((char)i);
            var text = String.Join("", Enumerable.Repeat(buffer.ToString(), 16));
            using (var template = new Template(new Java(),"[%=2+2%]"+text, new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("4"+text.ToString(),  output.ToString());
            }
        }

        [TestMethod]
        public void TwoÑycles()
        {
            using (var template = new Template(new Java(), "![%@n%]<[%@ n + m %]*[%@%]>[%@%]", new string[0],
                new Variable("n", ArgymentType.Int32, 2),
                new Variable("m", ArgymentType.Int32, 1)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("!<***><***>", output.ToString());
            }
        }
        
        [TestMethod]
        public void CodeCycleFor()
        {
            using (var template = new Template(new Java(), "[%for(int i =0;i<10;i++) System.out.print(i);%]", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("0123456789", output.ToString());
            }
        }
        [TestMethod]
        public void CodeForAndBoolType()           //4
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%]TRUE[%@3%]![%@%][%?%]", new string[0],
                new Variable() { Name = "s", Type = ArgymentType.String, Value = "TEST" }))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }
       

        
        [TestMethod]
        public void TextSimple()
        {
            using (var template = new Template(new Java(), "****", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("****", output.ToString());
            }

        }
        [TestMethod]
        public void BoolTypeCycleFor()
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%for(int i = 0; i < 5; i++){%][%=i%][%}%][%?%]",
                new string[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("01234", output.ToString());
            }

        }
        [TestMethod]
        public void BoolTypeCycleForAndText()
        {
            using (var template = new Template(new Java(), @"1[%?s.equals(""TEST"")%]1[%for(int i = 0; i < 5; i++){%]1[%=i%]1[%}%]1[%?%]1",
                new string[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("1110111112113114111", output.ToString());
            }

        }
        [TestMethod]
        public void TextAndVariable()
        {


            using (var template = new Template(new Java(), "****[%=i+i%]", new string[0],
                new Variable("i",ArgymentType.Int32,5)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("****10", output.ToString());
            }
        }

        [TestMethod]
        public void BoolTypeCycleForFrom0To10050()
        {
            var text = "";
            for (int i = 0; i < 100; i++)
            {
                text += i;
            }
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%for(int i = 0; i < 100; i++){%][%=i%][%}%][%?%]", new string[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual(text, output.ToString());
            }

        }
        [TestMethod]
        public void TwoÑyclesBig()
        {
            const int n = 2; const int m = 4;
            var text = "";
            for (var for0 = 0; for0 < 2; for0++)
            {
                text += ("<");
                for (var for1 = 0; for1 < n % m; for1++)
                {
                    text += ("*");
                }
                text += (">");
            }

            using (var template = new Template(new Java(), "[%@n%]<[%@ n % m %]*[%@%]>[%@%]", new string[0],
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
            
            using (var template = new Template(new Java(), @"[%=s.getWeeksInWeekYear()%]", new string[0], new Variable("s", ArgymentType.Calendar)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual("52", output.ToString());
            }

        }
        [TestMethod]
        public void CalendarAndCycleFor()
        {
          
            using (var template = new Template(new Java(), @"[% fo%][%r(int i = 0; i < 5; i++){ %][%=s.getWeekYear()%][%}%]", new string[0],
                new Variable("s", ArgymentType.Calendar)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual("20152015201520152015", output.ToString());
            }
        }

        [TestMethod]
        public void IntegerAndLong()
        {
            var text = "";
            const int n = 7;
            const long m = 3;
            for (var for0 = 0; for0 < 7; for0++)
            {
                text += ("<");
                for (var for1 = 0; for1 < n + m; for1++)
                {
                    text += ("*");

                } text += (">");
            }
            using (var template = new Template(new Java(), @"[%@n%]<[%@ n + m %]*[%@%]>[%@%]", new string[0],
                new Variable("n", ArgymentType.Int32, n),
                new Variable("m", ArgymentType.Int64, m)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual(text, output.ToString());
            }

        }
        [TestMethod]
        public void LongAndLong()
        {
            var t = "";
            const int n = 7;
            const long m = 3;
            for (var for0 = 0; for0 < 7; for0++)
            {
                t += ("<");
                for (var for1 = 0; for1 < n + m+n; for1++)
                {
                    t += ("*");

                } t += (">");
            }
            using (var template = new Template(new Java(), @"[%@n%]<[%@ n + m +n%]*[%@%]>[%@%]", new string[0],
                new Variable("n", ArgymentType.Int64, n),
                new Variable("m", ArgymentType.Int64, m)))
            using (var output = new StringWriter())
            {
                template.Render(output);

                Assert.AreEqual(t, output.ToString());
            }

        }
       
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
            using (var template = new Template(new Java(), @"[for(int i =0;i<10;i++)%][%@3%]<[%@ 3 + 7 %]*[%@%]>[%@%][%;%]", new string[0]))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual(t, output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimple()
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%]TRUE[%@3%]![%@%][%?%]", new string[0], new Variable("s", ArgymentType.String, "TEST")))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("TRUE!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimpleAndVariable()
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%=i%][%@3%]![%@%][%?%]", new string[0],
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("i", ArgymentType.Int32, 1)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("1!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimpleAndVariablesMath()
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%=java.lang.Math.pow(i,i)%][%@3%]![%@%][%?%]", new string[0],
                new Variable("s", ArgymentType.String, "TEST"),
                new Variable("i", ArgymentType.Int32, 2)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("4.0!!!", output.ToString());
            }
        }
        [TestMethod]
        public void BoolSimpleAndVariables()
        {
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%=i+i%][%@3%]![%@%][%?%]", new string[0],
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
            using (var template = new Template(new Java(), @"[%?s.equals(""TEST"")%][%for(int i = 0; i < Math.pow(3,2); i++){%][%=i%][%}%][%?%]", new string[0],
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
            using (var template = new Template(new Java(), @"[%?s.equals(""TESTTESTTEST%TESTTESTTESTTESTTESTTESTTEST"")%]TRUE[%@3%]![%@%][%?%]", new string[0],
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
            using (var template = new Template(new Java(), @"[%?s.equals(s.toString())%]TRUE[%@3%]![%@%][%?%]9", new string[0],
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
            using (var template = new Template(new Java(), @"[%@i%][%? i < e%][%=i+i%][%break;%][%?%][%@%]", new string[0],
                new Variable("i", ArgymentType.Int32, 4),
                new Variable("e", ArgymentType.Int32, 6)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("8", output.ToString());
            }
        }
        [TestMethod]
        public void TextCodeText()
        {
            using (var template = new Template(new Java(), @"text[%@i%][%? i < e%][%=i+i%][%break;%][%?%][%@%]text", new string[0],
                new Variable("i", ArgymentType.Int32, 4),
                new Variable("e", ArgymentType.Int32, 6)))
            using (var output = new StringWriter())
            {
                template.Render(output);
                Assert.AreEqual("text8text", output.ToString());
            }
        }
    }
}