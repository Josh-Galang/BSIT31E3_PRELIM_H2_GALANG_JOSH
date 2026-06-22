using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    /// <summary>
    /// Resolves the appropriate <see cref="IFileReader"/> implementation
    /// based on a requested format string.
    /// </summary>
    public class FileReaderResolver
    {
        /// <summary>
        /// Holds all registered reader instances that can be queried by format.
        /// </summary>
        private readonly List<IFileReader> _availableReaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReaderResolver"/> class
        /// and populates the list of available readers.
        /// </summary>
        public FileReaderResolver()
        {
            _availableReaders = new List<IFileReader>
            {
                new TextFileReader()
                // TODO: Register CsvFileReader, JsonFileReader, XmlFileReader here
            };
        }

        /// <summary>
        /// Returns the first registered reader whose <see cref="IFileReader.SupportedFormat"/>
        /// matches the specified format string (case-insensitive).
        /// </summary>
        /// <param name="format">The target format (e.g., "TXT", "CSV", "JSON", "XML").</param>
        /// <returns>An <see cref="IFileReader"/> if a match is found; otherwise, <c>null</c>.</returns>
        public IFileReader GetReaderForFormat(string format)
        {
            return _availableReaders.FirstOrDefault(r =>
                r.SupportedFormat.Equals(format, StringComparison.OrdinalIgnoreCase));
        }
    }
}