using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using wimm.Secundatives.ApplyFunctionsExtensions;
using Xunit;

namespace wimm.Secundatives.UnitTests.ApplyFunctionsExtensions
{
    public class ApplyFunctionsExtensions_Test
    {
        public class Apply
        {
            [Fact]
            public void NullTargetWithoutAllowNull_Throws()
            {
                var ex = Assert.Throws<ArgumentNullException>(() => (null as string).Apply(string.IsNullOrEmpty));
                Assert.Equal("target", ex.ParamName);
            }

            [Theory]
            [InlineData(false)]
            [InlineData(true)]
            public void NullFn_Throws(bool allowNull)
            {
                var ex = Assert.Throws<ArgumentNullException>(() => "fn".Apply(null as Func<string, bool>, allowNull));
                Assert.Equal("fn", ex.ParamName);
            }

            [Theory]
            [InlineData(null, true)]
            [InlineData("target", false)]
            [InlineData("target", true)]
            public void ValidArgs_AppliesFunction(object target, bool allowNull)
            {
                var fn = new Mock<Func<object, object>>();
                var expected = new object();
                fn.Setup(f => f(It.IsAny<object>())).Returns(expected);

                var actual = target.Apply(fn.Object, allowNull);

                fn.Verify(f => f(target), Times.Once);
                Assert.Same(expected, actual);
            }
        }

        public class In
        {
            private const string _bar = "bar";
            private readonly IEnumerable<string> _collectionWithoutTarget = new[] { "foo", "baz" };
            private readonly IEnumerable<string> _collectionWithTarget = new[] { "foo", _bar, null, "baz" };

            [Fact]
            public void NullTargetWithoutAllowNull_Throws()
            {
                var ex = Assert.Throws<ArgumentNullException>(() => (null as string).In(_collectionWithTarget));
                Assert.Equal("target", ex.ParamName);
            }

            [Theory]
            [InlineData(false)]
            [InlineData(true)]
            public void NullCollection_Throws(bool allowNull)
            {
                var ex = Assert.Throws<ArgumentNullException>(() => _bar.In(null, allowNull));
                Assert.Equal("enumerable", ex.ParamName);
            }

            [Theory]
            [InlineData(false)]
            [InlineData(true)]
            public void EmptyCollection_ReturnsFalse(bool allowNull)
            {
                Assert.False(_bar.In(Array.Empty<string>(), allowNull));
            }

            [Theory]
            [InlineData(null, true)]
            [InlineData(_bar, false)]
            [InlineData(_bar, true)]
            public void TargetNotInCollection_ReturnsFalse(string target, bool allowNull)
            {
                Assert.False(target.In(_collectionWithoutTarget, allowNull));
            }

            [Theory]
            [InlineData(null, true)]
            [InlineData(_bar, false)]
            [InlineData(_bar, true)]
            public void TargetInCollection_ReturnsTrue(string target, bool allowNull)
            {
                Assert.True(target.In(_collectionWithTarget, allowNull));
            }
        }
    }
}
