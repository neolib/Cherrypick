/*
 * A lightweight CSV reader class.
 *
 * Copyright © neolib.net. All rights reserved.
 *
 * */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace neolib.Files
{
    /// <summary>
    /// A lightweight CSV reader and parser.
    /// </summary>
    /// <remarks>
    /// This class implements two modes for reading CSV content:
    /// 1. Direct mode - read all records into a list of records in a single run.
    /// 2. Parser mode - read one record at a time and fire event when it's ready.
    ///
    /// The parser mode is desgined to use a minimal memory footprint and is
    /// intended for reading very large content.
    /// </remarks>
    public class CsvReader
    {
        #region Events

        public delegate void RecordReady(CsvRecord record);
        public event RecordReady OnRecordReady;

        #endregion

        #region Public Properties

        public char Delimiter { get; set; } = ',';
        public int TabWidth { get; set; } = 4;
        public bool Strict { get; set; }

        #endregion

        #region Direct Mode

        public CsvRecordList ReadText(string text)
        {
            var records = new CsvRecordList();
            this.OnRecordReady += (record) => records.Add(record);
            this.ParseText(text);
            return records;
        }

        public CsvRecordList ReadFile(string filename, Encoding encoding = null)
        {
            var records = new CsvRecordList();
            this.OnRecordReady += (record) => records.Add(record);
            this.ParseFile(filename, encoding);
            return records;
        }

        #endregion

        #region Parser Mode

        public void ParseText(string text)
        {
            using (var reader = new StringReader(text))
            {
                this.read(reader);
            }
        }

        public void ParseFile(string filename, Encoding encoding)
        {
            using (var reader = encoding == null
                ?
                new StreamReader(filename, true)
                :
                new StreamReader(filename, encoding))
            {
                this.read(reader);
            }
        }

        #endregion

        #region Actual Parse Method

        private void read(TextReader reader)
        {
            var fieldBuilder = new StringBuilder();
            var record = new CsvRecord();
            bool fieldQuoted = false;
            bool fieldStarted = false;
            int column = 0;
            int lines = 0;
            char lastChar = '\uffff';

            while (true)
            {
                int c = reader.Read();
                if (c == -1)
                {
                    collectRecord_();
                    break;
                }

                char thisChar = (char)c;

                fieldStarted = true;
                column += thisChar == '\t' ? this.TabWidth : 1;

                if (thisChar == '"')
                {
                    if (!fieldQuoted && fieldBuilder.Length == 0)
                    {
                        fieldQuoted = true;
                        lastChar = '\uffff';
                        continue;
                    }
                    else if (fieldQuoted)
                    {
                        if (lastChar == '"' && fieldBuilder.Length > 0)
                        {
                            fieldBuilder.Append(thisChar);
                            lastChar = '\uffff';
                            continue;
                        }
                    }
                    else
                    {
                        fieldBuilder.Append(thisChar);
                    }
                }
                else if (thisChar == this.Delimiter)
                {
                    if (fieldQuoted)
                    {
                        if (lastChar == '"')
                        {
                            collectField_();
                        }
                        else fieldBuilder.Append(thisChar);
                    }
                    else collectField_();
                }
                else if (thisChar == '\r')
                {
                    /*
                     * Special handling of \r\n sequqnce.
                     *
                     * */

                    int nextChar = reader.Peek();

                    if (nextChar == '\n' || nextChar == -1)
                    {
                        /*
                         * Next char is \n, so they form a line ending. Need to
                         * ignore current \r char and let next loop handle \n.
                         *
                         * */

                        // Do not remember this \r char.
                        continue;
                    }
                    else
                    {
                        /*
                         * Current char is a single \r, so treat it as if it's \n.
                         *
                         * */

                        handleLineEnding_(thisChar);
                    }
                }
                else if (thisChar == '\n')
                {
                    handleLineEnding_(thisChar);
                }
                else
                {
                    /*
                     * If previously we had an end of quote and now get a normal
                     * char, then it's a malformed field. We need to throw
                     * exception in strict mode, or workaround it by treating
                     * previous double-quote and the one at field begin as
                     * normal double-quotes.
                     *
                     * */

                    var malformed = fieldQuoted && lastChar == '"';
                    if (this.Strict && malformed)
                        throw new Exception($"Dangling double-quote at {lines + 1},{column - 1}.");

                    if (malformed)
                    {
                        fieldQuoted = false;
                        fieldBuilder.Insert(0, '"');
                        // Treat previous double-quote as normal char.
                        fieldBuilder.Append('"');
                    }
                    fieldBuilder.Append(thisChar);
                }

                lastChar = thisChar;
            }

            void handleLineEnding_(char endingChar_)
            {
                lines++;
                column = 0;
                if (fieldQuoted)
                {
                    if (lastChar == '"')
                    {
                        collectRecord_();
                    }
                    else fieldBuilder.Append(endingChar_);
                }
                else
                {
                    collectRecord_();
                }
            }

            bool collectField_()
            {
                if (fieldStarted)
                {
                    if (fieldQuoted)
                        record.Add(fieldBuilder.ToString());
                    else
                        record.Add(fieldBuilder.Length > 0 ? fieldBuilder.ToString() : null);

                    fieldBuilder.Clear();
                    fieldQuoted = false;
                    fieldStarted = false;
                    lastChar = '\uffff';
                    return true;
                }
                return false;
            }

            void collectRecord_()
            {
                collectField_();
                if (record.FieldCount > 0)
                {
                    this.OnRecordReady?.Invoke(record);
                    record = new CsvRecord();
                }
            }
        }

        #endregion
    }

    #region Entity Classes

    /// <summary>
    /// A container class for list of values.
    /// </summary>
    /// <remarks>
    /// The reason to create a separate class instead of a simple list for field values is that the
    /// List indexer would throw exception on accessing a value out of bound. This class has a
    /// custom indexer that simply returns null in this situation and at the same time caller can
    /// check the <see cref="FieldCount"/> property to get actual number of fields in this record.
    /// 
    /// The benefit is that developer won't worry about index out of bound exception; and, it is very
    /// convenient for reading malformed CSV content because all records may not have exactly the
    /// same number of fields (as indicated by the first row).
    /// </remarks>
    public class CsvRecord : IEnumerable<string>
    {
        private List<string> fields = new List<string>();

        public int FieldCount => fields.Count;

        public string this[int index] => index < fields.Count ? fields[index] : null;

        public void Add(string item) => fields.Add(item);

        #region IEnumerable

        public IEnumerator<string> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : IEnumerator<string>
        {
            private CsvRecord record;
            private int index = -1;

            internal Enumerator(CsvRecord record)
            {
                this.record = record;
            }

            public string Current => record[index];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (index < record.FieldCount - 1)
                {
                    index++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                index = -1;
            }
        }

        #endregion
    }

    /// <summary>
    /// A marker class for a list of CsvRecords.
    /// </summary>
    public class CsvRecordList : List<CsvRecord> { }

    #endregion

}
