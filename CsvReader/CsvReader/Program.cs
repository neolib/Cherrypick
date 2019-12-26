/*
 * Part of CsvReader library.
 *
 * Copyright © neolib.net. All rights reserved.
 *
 * */
using System;
using System.IO;

namespace CsvReader
{
    using CsvReaderLib;
    using neolib.Files;
    using static Console;

    class Program
    {
        static void Main(string[] args)
        {
            var r = new CsvReader { Strict = false };
            var sepLine = new string('=', 60);
            var printer = new CsvPrinter(Out);

            foreach (var file in args)
            {
                WriteLine($"Parsing {file}");
                WriteLine(sepLine);

                try
                {
                    if (File.Exists(file))
                    {
                        var records = r.ReadFile(file);
                        printer.Print(records);
                    }
                    else
                    {
                        WriteLine($"File does not exist: {file}");
                    }
                }
                catch (Exception ex)
                {
                    WriteLine(ex);
                }
                WriteLine();
            }

            WriteLine("Hit ENTER to exit...");
            ReadLine();
        }
    }
}
