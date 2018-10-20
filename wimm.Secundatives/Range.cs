using System;

namespace wimm.Secundatives
{
    /// <inheritDoc />
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        public T Min { get; }

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
