using System;
using Xunit;

namespace wimm.Secundatives.UnitTests
{
    public class TimeOfDay_Test
    {
        private static readonly TimeSpan _time = TimeSpan.FromHours(12);

        [Fact]
        public void Construct_NegativeTime_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new TimeOfDay(TimeSpan.FromMinutes(-1));
            });

            Assert.Equal("time", ex.ParamName);
        }

        [Fact]
        public void Construct_Zero_Constructs()
        {
            var _ = new TimeOfDay(TimeSpan.Zero);
        }

        [Fact]
        public void Construct_LessThan24Hours_Constructs()
        {
            var _ = new TimeOfDay(_time);
        }

        [Fact]
        public void Construct_24Hours_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new TimeOfDay(TimeSpan.FromHours(24));
            });

            Assert.Equal("time", ex.ParamName);
        }

        [Fact]
        public void Construct_MoreThan24Hours_Throws()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var _ = new TimeOfDay(TimeSpan.FromDays(1) + TimeSpan.FromMinutes(1));
            });

            Assert.Equal("time", ex.ParamName);
        }

        [Fact]
        public void CastToTimeSpan_ConstructedWithTimeSpan_ValueEqualsTimeSpan()
        {
            var underTest = new TimeOfDay(_time);

            TimeSpan actual = underTest;

            Assert.Equal(_time, actual);
        }

        [Fact]
        public void Construct_Default_IsZero()
        {
            var underTest = new TimeOfDay();

            Assert.Equal(TimeSpan.Zero, underTest);
        }
    }
}
