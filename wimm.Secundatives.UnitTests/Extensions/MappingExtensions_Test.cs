using System.Threading.Tasks;
using Xunit;

namespace wimm.Secundatives.UnitTests.Extensions
{
    public class MappingExtensions_Test
    {
        public class MapMaybe_FuncTToU
        {
            [Fact]
            public void None_ReturnsNone()
            {
                var underTest = Maybe<int>.None;
                var actual = underTest.Map(x => x.ToString());
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public void Some_ReturnsTransformedValue()
            {
                var underTest = new Maybe<int>(10);
                var actual = underTest.Map(x => x.ToString());
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapMaybe_FuncTToMaybeU
        {
            [Fact]
            public void None_ReturnsNone()
            {
                var underTest = Maybe<int>.None;
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public void SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = new Maybe<int>(10);
                var actual = underTest.Map(x => Maybe<string>.None);
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public void SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = new Maybe<int>(10);
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapMaybe_FuncTToTaskU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Maybe<int>.None;
                var actual = await underTest.Map(x => Task.FromResult(x.ToString()));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = new Maybe<int>(10);
                var actual = await underTest.Map(x => Task.FromResult(x.ToString()));
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapMaybe_FuncTToTaskMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Maybe<int>.None;
                var actual = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = new Maybe<int>(10);
                var actual = await underTest.Map(x => Task.FromResult(Maybe<string>.None));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = new Maybe<int>(10);
                var actual = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapTaskMaybe_FuncTToU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Task.FromResult(Maybe<int>.None);
                var actual = await underTest.Map(x => x.ToString());
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => x.ToString());
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapTaskMaybe_FuncTToMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Task.FromResult(Maybe<int>.None);
                var actual = await underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => Maybe<string>.None);
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapTaskMaybe_FuncTToTaskU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Task.FromResult(Maybe<int>.None);
                var actual = await underTest.Map(x => Task.FromResult(x.ToString()));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => Task.FromResult(x.ToString()));
                Assert.Equal("10", actual.Value);
            }
        }

        public class MapTaskMaybe_FuncTToTaskMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Task.FromResult(Maybe<int>.None);
                var actual = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => Task.FromResult(Maybe<string>.None));
                Assert.Equal(Maybe.None, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = Task.FromResult(new Maybe<int>(10));
                var actual = await underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                Assert.Equal("10", actual.Value);
            }
        }
    }
}
