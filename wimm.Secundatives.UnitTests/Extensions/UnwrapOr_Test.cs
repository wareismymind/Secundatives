using System;
using System.Threading.Tasks;
using wimm.Secundatives.Extensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class UnwrapOr_Test
    {
        private const string _valid = "doot";
        private const string _default = "tood";
        private readonly Func<string> _defaultFunc = () => { _set = true; return _default; };
        private readonly Func<Task<string>> _defaultAsyncFunc = async () => { _set = true; ; return await Task.FromResult(_default); };
        private static bool _set = false;

        public UnwrapOr_Test()
        {
            _set = false;
        }

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

        [Fact]
        public async Task UnwrapOrAsyncFunc_NoValue_ExecutesFunction()
        {
            var underTest = new Maybe<string>();
            Assert.Equal(_default, await underTest.UnwrapOr(_defaultAsyncFunc));
            Assert.True(_set);
        }

        [Fact]
        public async Task UnwrapOrAsyncFunc_Value_ReturnsValue()
        {
            var underTest = new Maybe<string>(_valid);
            Assert.Equal(_valid, await underTest.UnwrapOr(_defaultAsyncFunc));
        }

        [Fact]
        public async Task UnwrapOrAsyncFunc_Value_DoesNotExecuteFunction()
        {
            var underTest = new Maybe<string>(_valid);
            _ = await underTest.UnwrapOr(_defaultAsyncFunc);
            Assert.False(_set);
        }

        [Fact]
        public async Task UnwrapOrTaskMaybeAsyncFunc_Value_ReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<string>(_valid));
            Assert.Equal(_valid, await underTest.UnwrapOr(_defaultAsyncFunc));
        }

        [Fact]
        public async Task UnwrapOrTaskMaybeAsyncFunc_Value_DoesNotExecuteFunction()
        {
            var underTest = Task.FromResult(new Maybe<string>(_valid));
            _ = await underTest.UnwrapOr(_defaultAsyncFunc);
            Assert.False(_set);
        }

        [Fact]
        public async Task UnwrapOrTaskMaybeAsyncFunc_NoValue_ExecutesFunction()
        {
            var underTest = Task.FromResult(new Maybe<string>());
            Assert.Equal(_default, await underTest.UnwrapOr(_defaultAsyncFunc));
            Assert.True(_set);
        }

        [Fact]
        public async Task UnwrapOrTaskMaybe_Value_ReturnsValue()
        {
            var underTest = Task.FromResult(new Maybe<string>(_valid));
            Assert.Equal(_valid, await underTest.UnwrapOr(_defaultFunc));
        }
        
        [Fact]
        public async Task UnwrapOrTaskMaybe_Value_DoesNotExecuteFunction()
        {
            var underTest = Task.FromResult(new Maybe<string>(_valid));
            _ = await underTest.UnwrapOr(_defaultFunc);
            Assert.False(_set);
        }

        [Fact]
        public async Task UnwrapOrTaskMaybe_NoValue_ExecutesFunction()
        {
            var underTest = Task.FromResult(new Maybe<string>());
            Assert.Equal(_default, await underTest.UnwrapOr(_defaultFunc));
            Assert.True(_set);
        }
    }
}
