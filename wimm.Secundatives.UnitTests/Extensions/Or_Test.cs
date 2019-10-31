using Xunit;

using wimm.Secundatives.Extensions;
using System.Threading.Tasks;
using System;
using Moq;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class Or_Test
    {
        [Fact]
        public void Or_MaybeFunc_NeitherMaybeExists_ReturnsNone()
        {
            Assert.Equal(
                Maybe<int>.None,
                Maybe<int>.None.Or(() => Maybe<int>.None));
        }

        [Fact]
        public void Or_MaybeFunc_SecondMaybeExists_ReturnsSecondMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                Maybe<int>.None.Or(() => expected));
        }

        [Fact]
        public void Or_MaybeFunc_FirstMaybeExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                expected.Or(() => Maybe<int>.None));
        }

        [Fact]
        public void Or_MaybeFunc_BothMaybesExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                expected.Or(() => new Maybe<int>(43)));
        }

        [Fact]
        public void Or_MaybeFunc_BothMaybesExists_FuncDoesNotRun()
        {
            var expected = new Maybe<int>(42);
            var func = new Mock<Func<Maybe<int>>>();

            Assert.Equal(
                expected,
                expected.Or(func.Object));

            func.Verify(f => f.Invoke(), Times.Never);
        }

        [Fact]
        public async Task Or_MaybeFuncTask_NeitherMaybeExists_ReturnsNone()
        {
            Assert.Equal(
                Maybe<int>.None,
                await Maybe<int>.None.Or(() => Task.FromResult(Maybe<int>.None)));
        }

        [Fact]
        public async Task OrMaybeFuncTask_SecondMaybeExists_ReturnsSecondMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Maybe<int>.None.Or(() => Task.FromResult(expected)));
        }

        [Fact]
        public async Task Or_MaybeFuncTask_FirstMaybeExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await expected.Or(() => Task.FromResult(Maybe<int>.None)));
        }

        [Fact]
        public async Task Or_MaybeFuncTask_BothMaybesExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await expected.Or(() => Task.FromResult(new Maybe<int>(43))));
        }

        [Fact]
        public async Task Or_MaybeFuncTask_BothMaybesExists_FuncDoesNotRun()
        {
            var expected = new Maybe<int>(42);
            var func = new Mock<Func<Task<Maybe<int>>>>();

            Assert.Equal(
                expected,
                await expected.Or(func.Object));

            func.Verify(f => f.Invoke(), Times.Never);
        }

        [Fact]
        public async Task Or_TaskMaybeFunc_NeitherMaybeExists_ReturnsNone()
        {
            Assert.Equal(
                Maybe<int>.None,
                await Task.FromResult(Maybe<int>.None).Or(() => Maybe<int>.None));
        }

        [Fact]
        public async Task Or_TaskMaybeFunc_SecondMaybeExists_ReturnsSecondMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(Maybe<int>.None).Or(() => expected));
        }

        [Fact]
        public async Task Or_TaskMaybeFunc_FirstMaybeExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(() => Maybe<int>.None));
        }

        [Fact]
        public async Task Or_TaskMaybeFunc_BothMaybesExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(() => new Maybe<int>(43)));
        }

        [Fact]
        public async Task Or_TaskMaybeFunc_BothMaybesExists_FuncDoesNotRun()
        {
            var expected = new Maybe<int>(42);
            var func = new Mock<Func<Maybe<int>>>();

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(func.Object));

            func.Verify(f => f.Invoke(), Times.Never);
        }

        [Fact]
        public async Task Or_TaskMaybeFuncTask_NeitherMaybeExists_ReturnsNone()
        {
            Assert.Equal(
                Maybe<int>.None,
                await Task.FromResult(Maybe<int>.None).Or(() => Task.FromResult(Maybe<int>.None)));
        }

        [Fact]
        public async Task Or_TaskMaybeFuncTask_SecondMaybeExists_ReturnsSecondMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(Maybe<int>.None).Or(() => Task.FromResult(expected)));
        }

        [Fact]
        public async Task Or_TaskMaybeFuncTask_FirstMaybeExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(() => Task.FromResult(Maybe<int>.None)));
        }

        [Fact]
        public async Task Or_TaskMaybeFuncTask_BothMaybesExists_ReturnsFirstMaybe()
        {
            var expected = new Maybe<int>(42);

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(() => Task.FromResult(new Maybe<int>(43))));
        }

        [Fact]
        public async Task Or_TaskMaybeFuncTask_BothMaybesExists_FuncDoesNotRun()
        {
            var expected = new Maybe<int>(42);
            var func = new Mock<Func<Task<Maybe<int>>>>();

            Assert.Equal(
                expected,
                await Task.FromResult(expected).Or(func.Object));

            func.Verify(f => f.Invoke(), Times.Never);
        }
    }
}