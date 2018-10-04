using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class Range_Test
    {
        private const string _lessThanMin = "a";
        private const string _min = "b";
        private const string _betweenMinAndMax = "c";
        private const string _max = "d";
        private const string _greaterThanMax = "e";

        [Fact]
        public void ConstructMinMax_NullMin_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(null, _max);
            });

            Assert.Equal("min", ex.ParamName);
        }

        [Fact]
        public void ConstructMinMax_NullMax_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(_min, null);
            });

            Assert.Equal("max", ex.ParamName);
        }

        [Fact]
        public void ConstructMinMax_MaxIsLessThanMin_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var _ = new Range<string>(_min, _lessThanMin);
            });
        }

        [Fact]
        public void ConstructMinMax_MaxIsMin_Constructs()
        {
            var _ = new Range<string>(_min, _min);
        }

        [Fact]
        public void ConstructMinMax_MaxIsGreaterThanMin_Constructs()
        {
            var _ = new Range<string>(_min, _max);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConstructMinMaxExclusive_NullMin_Throws(bool exclusive)
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(null, _max, exclusive);
            });

            Assert.Equal("min", ex.ParamName);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConstructMinMaxExclusive_NullMax_Throws(bool exclusive)
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(_min, null, exclusive);
            });

            Assert.Equal("max", ex.ParamName);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConstructMinMaxExclusive_MaxIsLessThanMin_Throws(bool exclusive)
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var _ = new Range<string>(_min, _lessThanMin, exclusive);
            });
        }

        [Fact]
        public void ConstructMinMaxExclusive_NonExclusiveMaxIsMin_Constructs()
        {
            var _ = new Range<string>(_min, _max, false);
        }

        [Fact]
        public void ConstructMinMaxExclusive_ExclusiveMaxIsMin_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var _ = new Range<string>(_min, _min, true);
            });
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void ConstructMinMaxExclusive_MaxGreaterThanMin_Constructs(bool exclusive)
        {
            var _ = new Range<string>(_min, _max, exclusive);
        }

        [Fact]
        public void Min_ConstructMinMax_IsMin()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.Equal(_min, underTest.Min);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Min_ConstructMinMaxExclusive_IsMin(bool exclusive)
        {
            var underTest = new Range<string>(_min, _max, exclusive);
            Assert.Equal(_min, underTest.Min);
        }

        [Fact]
        public void Max_ConstructMinMax_IsMax()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.Equal(_max, underTest.Max);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Max_ConstructMinMaxExclusive_IsMax(bool exclusive)
        {
            var underTest = new Range<string>(_min, _max, exclusive);
            Assert.Equal(_max, underTest.Max);
        }

        [Fact]
        public void Exclusive_ConstructMinMax_IsFalse()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.False(underTest.Exclusive);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Exclusive_ConstructMinMaxExclusive_IsExclusive(bool exclusive)
        {
            var underTest = new Range<string>(_min, _max, exclusive);
            Assert.Equal(exclusive, underTest.Exclusive);
        }

        [Fact]
        public void Includes_NullValue_Throws()
        {
            var underTest = new Range<string>(_min, _max);

            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = underTest.Includes(null);
            });

            Assert.Equal("value", ex.ParamName);
        }

        [Fact]
        public void Includes_NonExclusiveValueIsLessThanMin_False()
        {
            var underTest = new Range<string>(_min, _max, false);
            Assert.False(underTest.Includes(_lessThanMin));
        }

        [Fact]
        public void Includes_NonExclusiveValueIsMin_True()
        {
            var underTest = new Range<string>(_min, _max, false);
            Assert.True(underTest.Includes(_min));
        }

        [Fact]
        public void Includes_NonExclusiveValueIsBetweenMinAndMax_True()
        {
            var underTest = new Range<string>(_min, _max, false);
            Assert.True(underTest.Includes(_betweenMinAndMax));
        }

        [Fact]
        public void Includes_NonExclusiveValueIsMax_True()
        {
            var underTest = new Range<string>(_min, _max, false);
            Assert.True(underTest.Includes(_max));
        }

        [Fact]
        public void Includes_NonExclusiveValueIsGreaterThanMax_True()
        {
            var underTest = new Range<string>(_min, _max, false);
            Assert.False(underTest.Includes(_greaterThanMax));
        }

        [Fact]
        public void Includes_ExclusiveValueIsLessThanMin_False()
        {
            var underTest = new Range<string>(_min, _max, true);
            Assert.False(underTest.Includes(_lessThanMin));
        }

        [Fact]
        public void Includes_ExclusiveValueIsMin_False()
        {
            var underTest = new Range<string>(_min, _max, true);
            Assert.False(underTest.Includes(_min));
        }

        [Fact]
        public void Includes_ExclusiveValueIsBetweenMinAndMax_True()
        {
            var underTest = new Range<string>(_min, _max, true);
            Assert.True(underTest.Includes(_betweenMinAndMax));
        }

        [Fact]
        public void Includes_ExclusiveValueIsMax_False()
        {
            var underTest = new Range<string>(_min, _max, true);
            Assert.False(underTest.Includes(_max));
        }

        [Fact]
        public void Includes_ExclusiveValueIsGreaterThanMax_True()
        {
            var underTest = new Range<string>(_min, _max, true);
            Assert.False(underTest.Includes(_greaterThanMax));
        }
    }
}
