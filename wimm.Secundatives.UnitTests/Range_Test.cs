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
        public void Construct_NullMin_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(null, _max);
            });

            Assert.Equal("min", ex.ParamName);
        }

        [Fact]
        public void Construct_NullMax_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new Range<string>(_min, null);
            });

            Assert.Equal("max", ex.ParamName);
        }

        [Fact]
        public void Construct_MaxIsLessThanMin_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var _ = new Range<string>(_min, _lessThanMin);
            });
        }

        [Fact]
        public void Construct_MaxIsMin_Constructs()
        {
            var _ = new Range<string>(_min, _min);
        }

        [Fact]
        public void Construct_MaxIsGreaterThanMin_Constructs()
        {
            var _ = new Range<string>(_min, _max);
        }

        [Fact]
        public void Min_ConstructMinMax_IsMin()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.Equal(_min, underTest.Min);
        }

        [Fact]
        public void Max_ConstructMinMax_IsMax()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.Equal(_max, underTest.Max);
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
        public void Includes_ValueIsLessThanMin_False()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.False(underTest.Includes(_lessThanMin));
        }

        [Fact]
        public void Includes_ValueIsMin_True()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.True(underTest.Includes(_min));
        }

        [Fact]
        public void Includes_ValueIsBetweenMinAndMax_True()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.True(underTest.Includes(_betweenMinAndMax));
        }

        [Fact]
        public void Includes_ValueIsMax_True()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.True(underTest.Includes(_max));
        }

        [Fact]
        public void Includes_ValueIsGreaterThanMax_True()
        {
            var underTest = new Range<string>(_min, _max);
            Assert.False(underTest.Includes(_greaterThanMax));
        }
    }
}
