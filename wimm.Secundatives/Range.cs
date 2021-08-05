using System;

namespace wimm.Secundatives
{
    /// <inheritDoc />
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        /// <summary>
        /// The inclusive lower bound of the range
        /// </summary>
        public T Min { get; }

        /// <summary>
        /// The inclusive upper bound of the range
        /// </summary>
        public T Max { get; }

        /// <summary>
        /// Initializes a new <see cref="Range{T}"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="min"/> or <paramref name="max"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="max"/> is less than <paramref name="min"/>.
        /// </exception>
        public Range(T min, T max)
        {
            Min = Require(min, nameof(min));
            Max = Require(max, nameof(max));

            if (max.CompareTo(min) < 0)
                throw new ArgumentException(
                    $"The value of {nameof(max)} must not be less than the value of {nameof(min)}.");
        }

        /// <summary>
        /// Determines if the given <typeparamref name="T"/> is between the lower and upper bounds of the <see cref="Range{T}"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns> <c>true</c> if <paramref name="value"/> is greater than <see cref="Max"/> and less than <see cref="Max"/> </returns>
        public bool Includes(T value)
        {
            Require(value, nameof(value));

            return !(Min.CompareTo(value) > 0 || Max.CompareTo(value) < 0);
        }

        private T Require(T value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
            return value;
        }
    }
}
