using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A range for an <see cref="IComparable{T}"/> value.
    /// </summary>
    public interface IRange<T> where T : IComparable<T>
    {
        /// <summary>
        /// The minimum value (inclusive).
        /// </summary>
        T Min { get; }

        /// <summary>
        /// The maximum value (inclusive).
        /// </summary>
        T Max { get; }

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
        bool Includes(T value);
    }
}
