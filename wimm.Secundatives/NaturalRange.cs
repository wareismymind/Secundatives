using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A <see cref="Range{T}"/> of natural numbers.
    /// </summary>
    /// <inheritDoc />
    public class NaturalRange : IRange<int>
    {
        private const string _outOfRangeMessage = "Must be greater than or equal to zero.";
        private readonly Range<int> _range;

        /// <summary>
        /// <see cref="Range{T}.Max"/>
        /// </summary>
        public int Max => _range.Max;

        /// <summary>
        /// <see cref="Range{T}.Min"/>
        /// </summary>
        public int Min => _range.Min;

        /// <summary>
        /// Initializes a new <see cref="NaturalRange"/> of 0 to <paramref name="max"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="max"/> is less than zero.
        /// </exception>
        public NaturalRange(int max) : this(0, max) { }

        /// <summary>
        /// Initializes a new <see cref="NaturalRange"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="min"/> or <paramref name="max"/> is less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="max"/> is less than <paramref name="min"/>.
        /// </exception>
        public NaturalRange(int min, int max)
        {
            if (min < 0) 
                throw new ArgumentOutOfRangeException(nameof(min), _outOfRangeMessage);
            if (max < 0) 
                throw new ArgumentOutOfRangeException(nameof(max), _outOfRangeMessage);

            if (max < min)
                throw new ArgumentException(
                    $"The value of {nameof(max)} must not be less than the value of {nameof(min)}.");

            _range = new Range<int>(min, max);
        }

        /// <summary>
        /// <see cref="Range{T}.Includes(T)"/>
        /// </summary>
        public bool Includes(int value) => _range.Includes(value);
    }
}
