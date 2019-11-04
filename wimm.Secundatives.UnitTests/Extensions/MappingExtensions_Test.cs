using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class MappingExtensions_Test
    {
        [Fact]
        public void MapMaybe_None_ReturnsNone()
        {
            var underTest = Maybe<int>.None;
            var result = underTest.Map(x => x + 100);

            Assert.Equal(Maybe<int>.None, result);
        }

        [Fact]
        public void MapMaybe_Value_ReturnsTransformed()
        {
            var underTest = new Maybe<int>(10);
            var result = underTest.Map(x => x.ToString());

            Assert.Equal("10", result.Value);
        }

        [Fact]
        public async Task MapMaybe_NoneAndFuncReturnsTask_ReturnsNoneTask()
        {
            var underTest = Maybe<int>.None;

            var result = await underTest.Map(x => Task.FromResult(x.ToString()));

            Assert.Equal(Maybe<string>.None, result);
        }

        [Fact]
        public async Task MapMaybe_ValueAndFuncReturnsTask_ReturnsValueTask()
        {
            var underTest = new Maybe<int>(10);
            var result = await underTest.Map(x => Task.FromResult(x.ToString()));

            Assert.Equal("10", result.Value);
        }

        [Fact]
        public void MapMaybe_NoneAndFuncReturnsMaybe_ReturnsNone()
        {
            var underTest = Maybe<int>.None;
            var result = underTest.Map(x => new Maybe<string>((x + 100).ToString()));

            Assert.Equal(Maybe<string>.None, result);

        }

        [Fact]
        public void MapMaybe_ValueAndFuncReturnsValue_ReturnsValue()
        {
            var underTest = new Maybe<int>(100);
            var result = underTest.Map(x => (x + 100).ToString());

            Assert.Equal("200", result.Value);
        }

        [Fact]
        public async Task MapTask_None_ReturnsNone()
        {
            var underTest = Task.FromResult(Maybe<int>.None);

            var result = await underTest.Map(x => x.ToString());
            Assert.Equal(Maybe<string>.None, result);
        }

        [Fact]
        public async Task MapTask_Value_ReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<int>(100));

            var result = await underTest.Map(x => x.ToString());

            Assert.Equal("100", result.Value);
        }

        [Fact]
        public async Task MapMaybe_NoneAndFuncReturnsAsyncMaybe_ReturnsNoneTask()
        {
            var underTest = Maybe<int>.None;
            var result = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));

            Assert.Equal(Maybe<string>.None, result);
        }

        [Fact]
        public async Task MapMaybe_ValueAndFuncReturnAsyncValue_ReturnsValueTask()
        {
            var underTest = new Maybe<int>(100);
            var result = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));

            Assert.Equal("100", result.Value);
        }

        [Fact]
        public async Task MapTask_ValueAndFuncReturnsAsyncValue_ReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<int>(100));

            var result = await underTest.Map(x => Task.FromResult(x.ToString()));

            Assert.Equal("100", result.Value);
        }

        [Fact]
        public async Task MapTask_NoneAndFuncReturnsAsyncValue_ReturnsNone()
        {
            var underTest = Task.FromResult(Maybe<int>.None);

            var result = await underTest.Map(x => Task.FromResult(x.ToString()));

            Assert.Equal(Maybe<string>.None, result);
        }

        [Fact]
        public async Task MapTask_ValueAndFuncReturnsAsyncMaybe_FlattensAndReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<int>(100));

            var result = await underTest.Map(x => Task.FromResult(x.ToString().AsMaybe()));

            Assert.Equal("100", result.Value);
        }

        [Fact]
        public async Task MapMaybe_NoneAndFuncReturnsAsyncMaybe_ReturnsNone()
        {
            var underTest = Task.FromResult(Maybe<int>.None);

            var result = await underTest.Map(x => Task.FromResult(x.ToString().AsMaybe()));

            Assert.Equal(Maybe<string>.None, result);
        }
    }
}
