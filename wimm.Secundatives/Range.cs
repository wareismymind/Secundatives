using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A range for an <see cref="IComparable{T}"/> value.
    /// </summary>
    public class Range<T> where T : IComparable<T>
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        public T Min { get; }

        /// <summary>
        /// The maximum value.
        /// </summary>
        public T Max { get; }

        /// <summary>
        /// Indicates whether <see cref="Min"/> and <see cref="Max"/> are excluded from the range.
        /// When <c>false</c>, values be greater than or equal to <see cref="Min"/> and less than
        /// or equal to <see cref="Max"/> are considered in range. When <c>true</c> values must be
        /// strictly greater than <see cref="Min"/> and less than <see cref="Max"/>.
        /// </summary>
        public bool Exclusive { get; }

        /// <summary>
        /// Initializes a new, non-exclusive <see cref="Range{T}"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="min"/> or <paramref name="max"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="max"/> is less than <paramref name="min"/>.
        /// </exception>
        public Range(T min, T max) : this(min, max, false) { }

        /// <summary>
        /// Initializes a new, non-exclusive <see cref="Range{T}"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="min"/> or <paramref name="max"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="max"/> is less than <paramref name="min"/>, or
        /// <paramref name="exclusive"/> is <c>true</c> and <paramref name="max"/> is equal to
        /// <paramref name="min"/>.
        /// </exception>
        public Range(T min, T max, bool exclusive)
        {
            Min = Require(min, nameof(min));
            Max = Require(max, nameof(max));
            Exclusive = exclusive;

            if (max.CompareTo(min) < 0)
                throw new ArgumentException(
                    $"The value of {nameof(max)} must not be less than the value of {nameof(min)}.");

            if (exclusive && max.CompareTo(min) == 0)
                throw new ArgumentException(
                    $"The value of {nameof(max)} must be greater than the value of {nameof(min)} for exclusive ranges.");
        }

        /// <summary>
        /// Indicates whether a value is included in the range.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public bool Includes(T value)
        {
            Require(value, nameof(value));

            if (Min.CompareTo(value) > 0 || Max.CompareTo(value) < 0)
                return false;

            if (Exclusive && (Min.CompareTo(value) == 0 || Max.CompareTo(value) == 0))
                return false;

            return true;
        }

        private T Require(T value, string name)
        {
            if (value == null) throw new ArgumentNullException(name);
            return value;
        }
    }
}
