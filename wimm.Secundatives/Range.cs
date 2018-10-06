using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A range for an <see cref="IComparable{T}"/> value.
    /// </summary>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>
        /// The minimum value (inclusive).
        /// </summary>
        public T Min { get; }

        /// <summary>
        /// The maximum value (inclusive).
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
        /// Indicates whether a value is included in the range.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="value"/> is equal to or greater than <see cref="Min"/>
        /// and less than or equal to <see cref="Max"/>, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
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
