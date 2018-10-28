using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Result_Test
    {
        private readonly Error _err = new Error("BadDoot");

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
        public void IsSome_ContainsValue_ReturnsTrue()
        {
            var underTest = ConstructInt();
            Assert.True(underTest.IsSome);
        }

        [Fact]
        public void IsSome_ContainsError_ReturnsFalse()
        {
            var underTest = ConstructErr();
            Assert.False(underTest.IsSome);
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

        
        private Result<int> ConstructInt()
        {
            return new Result<int>(42);
        }

        private Result<int> ConstructErr()
        {
            return new Result<int>(_err);
        }

    }
}
