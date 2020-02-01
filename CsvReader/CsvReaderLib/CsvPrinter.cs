/*
 * Part of CsvReader library.
 *
 * Copyright © neolib.net. All rights reserved.
 *
 * */
using neolib.Files;
using System.IO;
using System.Text;

namespace CsvReaderLib
{
    public class CsvPrinter
    {
        /// <summary>
        /// Escapes a string to a valid CSV value.
        /// </summary>
        /// <param name="s">Input string.</param>
        /// <param name="quote">Tri-state boolean value.</param>
        /// <returns>Escaped string.</returns>
        /// <remarks>
        /// Meaning of <paramref name="quote"/>:
        /// true
        ///     Always quote result value.
        /// false
        ///     Do not quote result value.
        /// null
        ///     Smart quote result value, only add quotes if <paramref name="s"/>
        ///     contains double-quote char.
        /// </remarks>
        public static string CsvEscape(string s, bool? quote = null)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var csv = s.Replace("\"", "\"\"");

            if (quote == true || (quote == null && s.IndexOf('"') >= 0)) return $"\"{csv}\"";
            return csv;
        }

        public TextWriter Output { get; set; }
        public string Separator { get; set; } = ",";
        public string SeparatorEscape { get; set; } = @"\,";
        public string NullEscape { get; set; } = "<0>";
        public string CrEscape { get; set; } = @"\r";
        public string LfEscape { get; set; } = @"\n";
        public string DoubleQuoteEscape { get; set; } = @"\""";

        public CsvPrinter(TextWriter output) { Output = output; }

        public string Escape(string s)
        {
            if (s == null) return NullEscape;
            if (string.IsNullOrWhiteSpace(s)) return s;

            var sb = new StringBuilder(s);

            Replace_(Separator, SeparatorEscape);
            Replace_("\r", CrEscape);
            Replace_("\n", LfEscape);
            Replace_("\"", DoubleQuoteEscape);

            return sb.ToString();

            void Replace_(string from, string to)
            {
                if (to?.Length > 0 && from != to)
                {
                    sb.Replace(from, to);
                }
            }
        }

        public void Print(string s)
        {
            Output.Write(Escape(s));
        }

        public void PrintLine(string s)
        {
            Output.WriteLine(Escape(s));
        }

        public void Print(CsvRecord record)
        {
            for (int i = 0; i < record.FieldCount - 1; i++)
            {
                Output.Write(Escape(record[i]));
                Output.Write(Separator);
            }
            Output.Write(Escape(record[record.FieldCount - 1]));
        }

        public void Print(CsvRecordList records)
        {
            foreach (var record in records)
            {
                Print(record);
                Output.WriteLine();
            }
        }

    }
}
