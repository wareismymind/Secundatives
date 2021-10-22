using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class Flatten_Test
    {
        [Fact]
        public void Flatten_OuterIsError_ReturnsError()
        {
            var err = 11;
            var underTest = new Result<Result<string, int>, int>(err);
            var res = underTest.Flatten();

            Assert.True(res.IsError);
            Assert.Equal(err, res.Error);
        }

        [Fact]
        public void Flatten_InnerExistsAndIsValue_ReturnsValue()
        {
            var value = "This is a value";
            var underTest = new Result<Result<string, int>, int>(new Result<string, int>("This is a value"));
            var res = underTest.Flatten();

            Assert.True(res.IsValue);
            Assert.Equal(value, res.Value);
        }


        [Fact]
        public void Flatten_InnerExistsAndIsError_ReturnsError()
        {
            var err = 10;
            var underTest = new Result<Result<string, int>, int>(new Result<string, int>(err));
            var res = underTest.Flatten();

            Assert.True(res.IsError);
            Assert.Equal(err, res.Error);
        }
    }
}
