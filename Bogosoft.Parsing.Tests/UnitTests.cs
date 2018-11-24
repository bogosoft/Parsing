using NUnit.Framework;
using Shouldly;
using System;

namespace Bogosoft.Parsing.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        static void ShouldThrow<T>(Action test) where T : Exception
        {
            test.ShouldThrow<T>();
        }

        [TestCase]
        public void ParseExtensionMethodThrowsArgumentNullExceptionWhenCurrentParserIsNull()
        {
            IParse<DateTime> parser = null;

            parser.ShouldBeNull();

            DateTime result;

            void Test()
            {
                result = parser.Parse("2018-11-22");
            }

            ShouldThrow<ArgumentNullException>(Test);
        }

        [TestCase]
        public void RegexParserReturnsArgumentNullExceptionOnParseAttemptWithNullString()
        {
            var parser = new RegexParser<int>("[0-9]+", m => int.Parse(m.Captures[0].Value));

            parser.ShouldNotBeNull();

            parser.TryParse(null, out int result, out Exception e).ShouldBeFalse();

            result.ShouldBe(default(int));

            e.ShouldNotBeNull();

            e.ShouldBeOfType<ArgumentNullException>();
        }

        [TestCase]
        public void TryParseExtensionMethodThrowsArgumentNullExceptionWhenCurrentParseIsNull()
        {
            IParse<DateTime> parser = null;

            parser.ShouldBeNull();

            bool success;

            void Test()
            {
                success = parser.TryParse("2018-11-22", out DateTime result);
            }

            ShouldThrow<ArgumentNullException>(Test);
        }
    }
}