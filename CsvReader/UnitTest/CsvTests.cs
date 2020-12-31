/*
 * Part of CsvReader library.
 *
 * Copyright © neolib.net. All rights reserved.
 *
 * */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    using static Console;
    using neolib.Files;
    using CsvReaderLib;

    [TestClass]
    public class CsvTests
    {
        [TestMethod]
        public void TestPrinter()
        {
            var printer = new CsvPrinter(Out);
            var a = new[]
            {
                null, printer.NullEscape,
                "", "",
                "abc", "abc",
                "ab,cd", $"ab{printer.SeparatorEscape}cd",
                "ab\"cd", $"ab{printer.DoubleQuoteEscape}cd",
                "a\rb\nc", $"a{printer.CrEscape}b{printer.LfEscape}"
            };

            for (int i = 0; i < a.Length / 2; i += 2)
            {
                Assert.AreEqual(printer.Escape(a[i]), a[i + 1]);
            }
        }

        [TestMethod]
        public void TestCorrectNess()
        {
            var csvReader = new CsvReader();
            var printer = new CsvPrinter(Out);
            printer.Print(csvReader.ReadFile(@"Files\correctness.csv"));
        }

        [TestMethod]
        public void TestGood()
        {
            var csvReader = new CsvReader();
            var printer = new CsvPrinter(Out);
            printer.Print(csvReader.ReadFile(@"Files\good.csv"));
        }

        [TestMethod]
        public void TestBad()
        {
            var csvReader = new CsvReader();
            var printer = new CsvPrinter(Out);
            printer.Print(csvReader.ReadFile(@"Files\bad.csv"));
        }

        [TestMethod]
        public void Test1000()
        {
            var csvReader = new CsvReader();
            var printer = new CsvPrinter(Out);
            printer.Print(csvReader.ReadFile(@"Files\1000.csv"));
        }
    }
}
