using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class TestHelpers
    {
        private static readonly List<string> _stringControls = new List<string> { "\t", "\r", "\n", " "};


        public static void AssertThrowsIfWhitespace(Action<string> action)
        {
            foreach (var control in _stringControls)
            {
                Assert.Throws<ArgumentException>(() => action(control));
            }
        }
    }
}
