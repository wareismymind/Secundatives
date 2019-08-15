using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wimm.Secundatives.Exceptions;
using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class Expect_Test
    {
        private const string _valid = "doot";
        private const string _message = "this is a message";

        [Fact]
        public void ExpectMessage_NullMessage_Throws()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Throws<ArgumentNullException>("message",() => underTest.Expect(null));
        }

        [Fact]
        public void ExpectMessage_WhiteSpaceMessage_Throws()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Throws<ArgumentException>("message",() => underTest.Expect(" \t\r\n"));
        }


        [Fact]
        public void ExpectMessage_DoesNotExist_ThrowsWithMessage()
        {
            var underTest = new Maybe<string>();
            var ex = Assert.Throws<ExpectException>(() => underTest.Expect(_message));

            Assert.Equal(_message, ex.Message);
        }

        [Fact]
        public void ExpectNoMessage_DoesNotExist_Throws()
        {
            var underTest = new Maybe<string>();
            var ex = Assert.Throws<ExpectException>(() => underTest.Expect());
        }

        [Fact]
        public void ExpectMessage_Exists_ReturnsValue()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Equal(_valid, underTest.Expect(_message));
        }

        [Fact]
        public void ExpectNoMessage_Exists_ReturnsValue()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Equal(_valid, underTest.Expect());
        }

        [Fact]
        public async Task ExpectTaskNoMessage_NoValue_Throws()
        {
            var underTest = Task.FromResult(Maybe<int>.None);
            await Assert.ThrowsAsync<ExpectException>(() => underTest.Expect());
        }

        [Fact]
        public async Task ExpectTaskNoMessage_Value_ReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<int>(10));
            Assert.Equal(10, await underTest.Expect());
        }

        [Fact]
        public async Task ExpectTaskMessage_NoValue_ThrowsWithMessage()
        {
            var underTest = Task.FromResult(Maybe<int>.None);
            var ex = await Assert.ThrowsAsync<ExpectException>(() => underTest.Expect(_message));

            Assert.Equal(_message, ex.Message);
        }


    }
}
