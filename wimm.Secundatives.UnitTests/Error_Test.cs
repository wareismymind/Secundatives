using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{

    public class Error_Test
    {
        [Fact]
        public void Construct_MessageNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Error(null));
        }

        [Fact]
        public void Construct_MessageWhitespace_Throws()
        {
            TestHelpers.AssertThrowsIfWhitespace(x => new Error(x));
        }

        [Fact]
        public void Construct_MessageValid_Constructs()
        {
            var err = new Error("I am a detailed error message: doot failed to construct");
            Assert.NotNull(err.Message);
        }
    }
}
