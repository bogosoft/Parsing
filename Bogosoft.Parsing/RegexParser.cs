using System;
using System.Text.RegularExpressions;

namespace Bogosoft.Parsing
{
    /// <summary>
    /// An implementation of the <see cref="IParse{T}"/> contract that relies on regular expressions
    /// to parse a given string.
    /// </summary>
    /// <typeparam name="T">The type of the object that results from parsing.</typeparam>
    public sealed class RegexParser<T> : IParse<T>
    {
        readonly Regex expression;
        readonly Func<Match, T> onmatch;

        /// <summary>
        /// Create a new instance of the <see cref="RegexParser{T}"/> class. Calling this constructor is equivalent
        /// to calling <see cref="RegexParser(string, RegexOptions, Func{Match, T})"/> with a value of
        /// <see cref="RegexOptions.Compiled"/>.
        /// </summary>
        /// <param name="pattern">A regular expression pattern.</param>
        /// <param name="onmatch">
        /// A delegate to be called on a successful match and which will be invoked to build the parsed object
        /// </param>
        public RegexParser(string pattern, Func<Match, T> onmatch)
            : this(pattern, RegexOptions.Compiled, onmatch)
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="RegexParser{T}"/> class.
        /// </summary>
        /// <param name="pattern">A regular expression pattern.</param>
        /// <param name="options">A collection of regular expression options.</param>
        /// <param name="onmatch">
        /// A delegate to be called on a successful match and which will be invoked to build the parsed object
        /// </param>
        public RegexParser(string pattern, RegexOptions options, Func<Match, T> onmatch)
            : this(new Regex(pattern, options), onmatch)
        {
        }

        /// <summary>
        /// Create a new instance of the <see cref="RegexParser{T}"/> class.
        /// </summary>
        /// <param name="expression">A regular expression.</param>
        /// <param name="onmatch">
        /// A delegate to be called on a successful match and which will be invoked to build the parsed object
        /// </param>
        public RegexParser(Regex expression, Func<Match, T> onmatch)
        {
            this.expression = expression;
            this.onmatch = onmatch;
        }

        /// <summary>
        /// Attempt to parse a given string into an object of the specified type.
        /// </summary>
        /// <param name="data">A string to parse.</param>
        /// <param name="result">The result of the parse operation.</param>
        /// <param name="exception">
        /// Any exception that occurred during the parse operation. If the given string is null,
        /// this will parameter will contain an <see cref="ArgumentNullException"/>.
        /// </param>
        /// <returns>A value indicating whether or not the parse operation succeeded.</returns>
        public bool TryParse(string data, out T result, out Exception exception)
        {
            exception = null;
            result = default(T);

            if (data is null)
            {
                exception = new ArgumentNullException(nameof(data));

                return false;
            }

            try
            {
                var match = expression.Match(data);

                if (match.Success)
                {
                    result = onmatch.Invoke(match);

                    return true;
                }
            }
            catch (Exception e)
            {
                exception = e;
            }

            return false;
        }
    }
}