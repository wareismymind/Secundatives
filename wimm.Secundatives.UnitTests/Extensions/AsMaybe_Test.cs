using System.Threading.Tasks;
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

            Assert.Equal(res.Value, value);
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

            Assert.Equal(value.Value, res.Value);
        }

        [Fact]
        public async Task AsMaybeTaskClass_Null_ReturnsNone()
        {
            var value = Task.FromResult((string)null);

            var res = await value.AsMaybe();

            Assert.Equal(Maybe<string>.None, res);
        }

        [Fact]
        public async Task AsMaybeTaskClass_Value_ReturnsSome()
        {
            var value = Task.FromResult("doot");

            var res = await value.AsMaybe();

            Assert.Equal("doot", res.Value);
        }

        [Fact]
        public async Task AsMaybeTaskNullable_Null_ReturnsNone()
        {
            var value = Task.FromResult((int?)null);

            var res = await value.AsMaybe();

            Assert.Equal(Maybe<int>.None, res);
        }

        [Fact]
        public async Task AsMaybeTaskNullable_Value_ReturnsSome()
        {
            var value = Task.FromResult((int?)25);

            var res = await value.AsMaybe();

            Assert.Equal(25, res.Value);
        }
    }
}
