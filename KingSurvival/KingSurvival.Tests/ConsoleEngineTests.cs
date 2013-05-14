namespace KingSurvival.Tests
{
    using System;
    using System.IO;
    using KingSurvival.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConsoleEngineTests
    {
        private const char Enter = (char)13;

        [TestMethod]
        public void TestKingWinsWithoutInvalidMoves()
        {
            string inputCommands = string.Format(
                "kul{0}bdr{0}kul{0}bdl{0}kur{0}adr{0}kur{0}bdl{0}kur{0}bdl{0}kul{0}ddl{0}kul", 
                Environment.NewLine);

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                using (StringReader stringReader = new StringReader(inputCommands))
                {
                    Console.SetIn(stringReader);

                    IEngine engine = new ConsoleEngine();
                    engine.Run();

                    char[] separators = Environment.NewLine.ToCharArray();
                    string consoleOutput = stringWriter.ToString();
                    string[] outputLines = consoleOutput.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    string expected = "King wins in 7 turns!";
                    int lastLineIndex = outputLines.Length - 1;
                    string actual = outputLines[lastLineIndex];
                    Assert.AreEqual<string>(expected, actual);
                }
            }
        }

        [TestMethod]
        public void TestKingWinsWithInvalidMoves()
        {
            string inputCommands = string.Format(
                ("kur{0}bdr{0}kur{0}bdr{0}kul{0}bdr{0}kur{0}{1}kul{0}bdr{0}" +
                "kdr{0}bdr{0}kul{0}bdr{0}{1}bdl{0}kur{0}bdr{0}kul{0}cdr{0}kur"),
                Environment.NewLine, Enter);

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                using (StringReader stringReader = new StringReader(inputCommands))
                {
                    Console.SetIn(stringReader);

                    IEngine engine = new ConsoleEngine();
                    engine.Run();

                    char[] separators = Environment.NewLine.ToCharArray();
                    string consoleOutput = stringWriter.ToString();
                    string[] outputLines = consoleOutput.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    string expected = "King wins in 9 turns!";
                    int lastLineIndex = outputLines.Length - 1;
                    string actual = outputLines[lastLineIndex];
                    Assert.AreEqual<string>(expected, actual);
                }
            }
        }

        [TestMethod]
        public void TestKingIsTrappedInRightDownCorner()
        {
            string inputCommands = string.Format(
                "kur{0}bdl{0}kur{0}bdr{0}kul{0}bdr{0}kdr{0}bdr{0}kdr{0}bdr{0}kdr{0}bdr",
                Environment.NewLine);

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);

                using (StringReader stringReader = new StringReader(inputCommands))
                {
                    Console.SetIn(stringReader);

                    IEngine engine = new ConsoleEngine();
                    engine.Run();

                    char[] separators = Environment.NewLine.ToCharArray();
                    string consoleOutput = stringWriter.ToString();
                    string[] outputLines = consoleOutput.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    string expected = "King loses in 6 turns...";
                    int lastLineIndex = outputLines.Length - 1;
                    string actual = outputLines[lastLineIndex];
                    Assert.AreEqual<string>(expected, actual);
                }
            }
        }
    }
}
