using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class AsMaybe_Test
    {
        [Fact]
        public void AsMaybeClass_NullValue_ReturnsNone()
        {
            string value = null;

            var res = value.AsMaybe();

            Assert.Equal(Maybe<string>.None, res);
        }

        [Fact]
        public void AsMaybeClass_Value_ReturnsSome()
        {
            var value = "doot";
            var res = value.AsMaybe();

            Assert.True(res.Exists);
        }

        [Fact]
        public void AsMaybeNullable_NullValue_ReturnsNone()
        {
            int? value = null;

            var res = value.AsMaybe();

            Assert.Equal(Maybe<int>.None, res);
        }

        [Fact]
        public void AsMaybeNullable_Value_ReturnsSome()
        {
            int? value = 10;

            var res = value.AsMaybe();

            Assert.True(res.Exists);
        }


    }
}
