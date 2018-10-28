using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class UnwrapOr_Test
    {
        private const string _valid = "doot";
        private const string _default = "tood";
        private readonly Func<string> _defaultFunc = () => _default;

        [Fact]
        public void UnwrapOr_Value_ReturnsValue()
        {
            var underTest = new Maybe<string>(_valid);

            Assert.Equal(_valid, underTest.UnwrapOr(_default));
        }

        [Fact]
        public void UnwrapOrFunc_Value_ReturnsValue()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Equal(_valid, underTest.UnwrapOr(_defaultFunc));
        }

        [Fact]
        public void UnwrawpOrFunc_NoValue_ExecutesFunction()
        {
            var underTest = new Maybe<string>();
            Assert.Equal(_default, underTest.UnwrapOr(_defaultFunc));
        }


        [Fact]
        public void UnwrapOr_NoValue_ReturnsDefaultParam()
        {
            var underTest = new Maybe<string>();
            Assert.Equal(_default, underTest.UnwrapOr(_default));
        }
    }
}
