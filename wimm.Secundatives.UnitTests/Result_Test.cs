using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Result_Test
    {
        private readonly Error _err = new("BadDoot");

        [Fact]
        public void Error_ContainsValue_Throws()
        {
            var underTest = ConstructInt();
            Assert.Throws<InvalidOperationException>(() => underTest.Error);
        }

        [Fact]
        public void Value_ContainsError_Throws()
        {
            var underTest = ConstructErr();
            Assert.Throws<InvalidOperationException>(() => underTest.Value);
        }

        [Fact]
        public void Error_ContainsError_ReturnsError()
        {
            var underTest = ConstructErr();
            Assert.Equal(_err, underTest.Error);
        }

        [Fact]
        public void Value_ContainsValue_ReturnsValue()
        {
            var underTest = ConstructInt();
            Assert.Equal(42, underTest.Value);
        }

        [Fact]
        public void IsValue_ContainsValue_ReturnsTrue()
        {
            var underTest = ConstructInt();
            Assert.True(underTest.IsValue);
        }

        [Fact]
        public void IsValue_ContainsError_ReturnsFalse()
        {
            var underTest = ConstructErr();
            Assert.False(underTest.IsValue);
        }

        [Fact]
        public void IsError_ContainsError_ReturnsTrue()
        {
            var underTest = ConstructErr();
            Assert.True(underTest.IsError);
        }

        [Fact]
        public void IsError_ContainsValue_ReturnsFalse()
        {
            var underTest = ConstructInt();
            Assert.False(underTest.IsError);
        }

        [Fact]
        public void Err_ContainsValue_ReturnsNone()
        {
            var underTest = ConstructInt();
            Assert.False(underTest.Err().Exists);
        }

        [Fact]
        public void Err_ContainsError_ReturnsSome()
        {
            var underTest = ConstructErr();
            Assert.Equal(underTest.Error, underTest.Err().Value);
        }

        [Fact]
        public void Ok_ContainsValue_ReturnsSome()
        {
            var underTest = ConstructInt();
            Assert.Equal(underTest.Value, underTest.Ok().Value);
        }

        [Fact]
        public void Ok_ContainsError_ReturnsNone()
        {
            var underTest = ConstructErr();
            Assert.False(underTest.Ok().Exists);
        }
        private static Result<int> ConstructInt()
        {
            return 42;
        }

        private Result<int> ConstructErr()
        {
            return _err;
        }
    }
}
