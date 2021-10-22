using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wimm.Secundatives.EnumerableExtensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.EnumerableExtensions
{
    public class EnumerableExtensions_Test
    {
        public class MapEnumerableMaybe_FuncTToU
        {
            [Fact]
            public void None_ReturnsNone()
            {
                var underTest = Enum(Maybe<int>.None);
                var expected = Enum(Maybe<string>.None);
                var actual = underTest.Map(x => x.ToString());
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void Some_ReturnsTransformedValue()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = Enum(new Maybe<string>("10"));
                var actual = underTest.Map(x => x.ToString());
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void MultipleCases_AreMappedAsExpected()
            {
                var underTest = Enum(Maybe.None, new Maybe<int>(10));
                var expected = Enum(Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => x.ToString());
                Assert.Equal(expected, actual);
            }
        }

        public class MapEnumerableMaybe_FuncTToMaybeU
        {
            [Fact]
            public void None_ReturnsNone()
            {
                var underTest = Enum(Maybe<int>.None);
                var expected = Enum(Maybe<string>.None);
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = Enum(Maybe<string>.None);
                var actual = underTest.Map(x => Maybe<string>.None);
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = Enum(new Maybe<string>("10"));
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                Assert.Equal(expected, actual);
            }

            [Fact]
            public void MultipleCases_AreMappedAsExpected()
            {
                var underTest = Enum(Maybe.None, new Maybe<int>(9), new Maybe<int>(10));
                var expected = Enum(Maybe.None, Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => x % 2 == 0 ? x.ToString() : Maybe<string>.None);
                Assert.Equal(expected, actual);
            }
        }

        public class MapEnumerableMaybe_FuncTToTaskU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Enum(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = Enum(Maybe.None, new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }
        }

        public class MapEnumerableMaybe_FuncTToTaskMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = Enum(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(Maybe<string>.None));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = Enum(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = Enum(Maybe.None, new Maybe<int>(9), new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x % 2 == 0 ? x.ToString() : Maybe<string>.None));
                await AssertResultsEqual(expected, actual);
            }
        }

        public class MapEnumerableTaskMaybe_FuncTToU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = EnumTasks(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => x.ToString());
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => x.ToString());
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = EnumTasks(Maybe.None, new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => x.ToString());
                await AssertResultsEqual(expected, actual);
            }
        }

        public class MapEnumerableTaskMaybe_FuncTToMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = EnumTasks(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Maybe<string>.None);
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => new Maybe<string>(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = EnumTasks(Maybe.None, new Maybe<int>(9), new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => x % 2 == 0 ? x.ToString() : Maybe<string>.None);
                await AssertResultsEqual(expected, actual);
            }
        }

        public class MapEnumerableTaskMaybe_FuncTToTaskU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = EnumTasks(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task Some_ReturnsTransformedValue()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = EnumTasks(Maybe.None, new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x.ToString()));
                await AssertResultsEqual(expected, actual);
            }
        }

        public class MapEnumerableTaskMaybe_FuncTToTaskMaybeU
        {
            [Fact]
            public async Task None_ReturnsNone()
            {
                var underTest = EnumTasks(Maybe<int>.None);
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsNone_ReturnsNone()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(Maybe<string>.None);
                var actual = underTest.Map(x => Task.FromResult(Maybe<string>.None));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task SomeAndTransformReturnsSome_ReturnsSome()
            {
                var underTest = EnumTasks(new Maybe<int>(10));
                var expected = EnumTasks(new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(new Maybe<string>(x.ToString())));
                await AssertResultsEqual(expected, actual);
            }

            [Fact]
            public async Task MultipleCases_AreMappedAsExpected()
            {
                var underTest = EnumTasks(Maybe.None, new Maybe<int>(9), new Maybe<int>(10));
                var expected = EnumTasks(Maybe.None, Maybe.None, new Maybe<string>("10"));
                var actual = underTest.Map(x => Task.FromResult(x % 2 == 0 ? x.ToString() : Maybe<string>.None));
                await AssertResultsEqual(expected, actual);
            }
        }
        private static IEnumerable<T> Enum<T>(params T[] elements) => elements.AsEnumerable();

        private static IEnumerable<Task<T>> EnumTasks<T>(params T[] elements) =>
            elements.Select(e => Task.FromResult(e)).AsEnumerable();

        private static async Task AssertResultsEqual<T>(IEnumerable<Task<T>> expected, IEnumerable<Task<T>> actual)
        {
            await Task.WhenAll(actual);
            Assert.Equal(expected.Select(e => e.Result), actual.Select(a => a.Result));
        }
    }
}
