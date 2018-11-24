using System;

namespace Bogosoft.Parsing
{
    /// <summary>
    /// A set of static members for working with <see cref="IParse{T}"/> types.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse a given string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object that results from parsing.</typeparam>
        /// <param name="parser">The current parser.</param>
        /// <param name="data">A string to parse.</param>
        /// <returns>An object of the parsed type if successful.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the current parser is null.
        /// </exception>
        /// <exception cref="Exception">
        /// Thrown if, when calling the current parser's <see cref="IParse{T}.TryParse(string, out T, out Exception)"/>
        /// method, the exception out argument was non null.
        /// </exception>
        /// <exception cref="FormatException">
        /// Thrown in the event that parsing failed but the underlying parser did not return an exception.
        /// </exception>
        public static T Parse<T>(this IParse<T> parser, string data)
        {
            if (parser is null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            if (parser.TryParse(data, out T result, out Exception e))
            {
                return result;
            }
            else if (e != null)
            {
                throw e;
            }
            else
            {
                throw new FormatException($"Given string, '{data}', did not match an expected format.");
            }
        }

        /// <summary>
        /// Attempt to parse a given string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object that results from parsing.</typeparam>
        /// <param name="parser">The current parser.</param>
        /// <param name="data">A string to parse.</param>
        /// <param name="result">The result of the parse operation.</param>
        /// <returns>A value indicating whether or not the parse operation succeeded.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown in the event that the current parser is null.
        /// </exception>
        public static bool TryParse<T>(this IParse<T> parser, string data, out T result)
        {
            if (parser is null)
            {
                throw new ArgumentNullException(nameof(parser));
            }

            return parser.TryParse(data, out result, out Exception ignored);
        }
    }
}