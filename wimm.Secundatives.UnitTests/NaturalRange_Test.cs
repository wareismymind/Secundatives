using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class NaturalRange_Test
    {
        private const int _min = 1;
        private const int _max = 3;

        [Fact]
        public void ConstructMax_MaxIsLessThanZero_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>("max", () => new NaturalRange(-1));
        }

        [Fact]
        public void ConstructMax_ValidArgs_Constructs()
        {
            var _ = new NaturalRange(_max);
        }

        [Fact]
        public void ConstructMax_MaxIsZero_Constructs()
        {
            var _ = new NaturalRange(0);
        }

        [Fact]
        public void ConstructMinMax_MinIsLessThanZero_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>("min", () => new NaturalRange(-1, _max));
        }

        [Fact]
        public void ConstructMinMax_MaxIsLessThanZero_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>("max", () => new NaturalRange(_min, -1));
        }

        [Fact]
        public void ConstructMinMax_MaxIsLessThanMin_Throws()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                var _ = new NaturalRange(3, 2);
            });
        }

        [Fact]
        public void ConstructMinMax_ValidArgs_Constructs()
        {
            var _ = new NaturalRange(_min, _max);
        }

        [Fact]
        public void ConstructMinMax_MinIsZero_Constructs()
        {
            var _ = new NaturalRange(0, _max);
        }

        [Fact]
        public void Construct_MaxIsMin_Constructs()
        {
            var _ = new NaturalRange(_min, _min);
        }

        [Fact]
        public void ConstructMinMax_MaxIsZero_Constructs()
        {
            // min must also be zero so that max >= min
            var _ = new NaturalRange(0, 0);
        }

        [Fact]
        public void Min_ConstructMax_IsZero()
        {
            var underTest = new NaturalRange(_max);
            Assert.Equal(0, underTest.Min);
        }

        [Fact]
        public void Min_ConstructMinMax_IsMin()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.Equal(_min, underTest.Min);
        }

        [Fact]
        public void Max_ConstrucMax_IsMax()
        {
            var underTest = new NaturalRange(_max);
            Assert.Equal(_max, underTest.Max);
        }

        [Fact]
        public void Max_ConstructMinMax_IsMax()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.Equal(_max, underTest.Max);
        }

        [Fact]
        public void Includes_ValueIsLessThanMin_False()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.False(underTest.Includes(_min - 1));
        }

        [Fact]
        public void Includes_ValueIsMin_True()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.True(underTest.Includes(_min));
        }

        [Fact]
        public void Includes_ValueIsBetweenMinAndMax_True()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.True(underTest.Includes(_min + 1));
        }

        [Fact]
        public void Includes_ValueIsMax_True()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.True(underTest.Includes(_max));
        }

        [Fact]
        public void Includes_ValueIsGreaterThanMax_True()
        {
            var underTest = new NaturalRange(_min, _max);
            Assert.False(underTest.Includes(_max + 1));
        }
    }
}
