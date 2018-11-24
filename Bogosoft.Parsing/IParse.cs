using System;

namespace Bogosoft.Parsing
{
    /// <summary>
    /// Represents any type capable of parsing strings into objects of a specified type
    /// </summary>
    /// <typeparam name="T">The type of the object that results from parsing.</typeparam>
    public interface IParse<T>
    {
        /// <summary>
        /// Attempt to parse a given string into an object of the specified type.
        /// </summary>
        /// <param name="data">A string to parse.</param>
        /// <param name="result">The result of the parse operation.</param>
        /// <param name="exception">Any exception that occurred during the parse operation.</param>
        /// <returns>A value indicating whether or not the parse operation succeeded.</returns>
        bool TryParse(string data, out T result, out Exception exception);
    }
}