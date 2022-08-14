using System;
using System.Collections.Generic;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class TestHelpers
    {
        private static readonly List<string> _stringControls = new() { "\t", "\r", "\n", " " };

        public static void AssertThrowsIfWhitespace(Action<string> action)
        {
            foreach (var control in _stringControls)
            {
                Assert.Throws<ArgumentException>(() => action(control));
            }
        }

        public static void AssertThrowsIfWhitespace<T>(Func<string, T> action)
        {
            foreach (var control in _stringControls)
            {
                Assert.Throws<ArgumentException>(() => action(control));
            }
        }
    }
}
