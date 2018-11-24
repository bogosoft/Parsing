using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Parsing
{
    /// <summary>
    /// An implementation of the <see cref="IParse{T}"/> that treats a collection
    /// of parsers as if they were a single parser. This class cannot be inherited.
    /// </summary>
    public sealed class CompositeParser<T> : IParse<T>
    {
        readonly IParse<T>[] parsers;

        /// <summary>
        /// Create a new instance of the <see cref="CompositeParser{T}"/> class.
        /// </summary>
        /// <param name="parsers">A collection or parsers to form the new composite.</param>
        public CompositeParser(IEnumerable<IParse<T>> parsers)
        {
            this.parsers = parsers.ToArray();
        }

        /// <summary>
        /// Create a new instance of the <see cref="CompositeParser{T}"/> class.
        /// </summary>
        /// <param name="parsers">An array of parsers to form the new composite.</param>
        public CompositeParser(params IParse<T>[] parsers)
        {
            this.parsers = parsers;
        }

        /// <summary>
        /// Attempt to parse a given string into an object of the specified type.
        /// </summary>
        /// <param name="data">A string to parse.</param>
        /// <param name="result">The result of the parse operation.</param>
        /// <param name="exception">Any exception that occurred during the parse operation.</param>
        /// <returns>A value indicating whether or not the parse operation succeeded.</returns>
        /// <remarks>
        /// When parsing, each parser of the composite will be called in the order they were provided
        /// to the constructor. The first parser to successfully parse the given string, or the first
        /// parser to encounter an exception, will short-circuit the iteration process and return
        /// immediately.
        /// </remarks>
        public bool TryParse(string data, out T result, out Exception exception)
        {
            exception = null;
            result = default(T);

            foreach (var parser in parsers)
            {
                if (parser.TryParse(data, out result, out exception))
                {
                    return true;
                }
                else if (exception != null)
                {
                    return false;
                }
            }

            return false;
        }
    }
}