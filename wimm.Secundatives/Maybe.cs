using System;
using System.Collections.Generic;

namespace wimm.Secundatives
{
    /// <summary>
    /// Static holder class for the constant <see cref="None"/>. 
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// A static instance of None to reduce memory pressure and to facilitate simple conversions
        /// </summary>
        public static readonly None None = new();
    }

    /// <summary>
    /// Class that represents that a value may or may not exist
    /// </summary>
    /// <typeparam name="T"> The type of the internal value</typeparam>
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        /// <summary>
        /// A helper instance representing a <see cref="Maybe{T}"/> with no value
        /// </summary>
        /// <remarks> Equivalent to default initialization of <see cref="Maybe{T}"/></remarks>
        public readonly static Maybe<T> None;

        /// <summary>
        /// Indicates whether or not the <see cref="Maybe{T}"/> has a value.
        /// </summary>
        public bool Exists { get; }

        /// <summary>
        /// The value of maybe object
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The maybe contains no value.
        /// </exception>
        public T Value => Exists
            ? _value
            : throw new InvalidOperationException("Value called on maybe with no value");

        private readonly T _value;

        /// <summary>
        /// Constructs a new <see cref="Maybe{T}"/> from the specified value
        /// </summary>
        /// <param name="value">The value</param>
        public Maybe(T value)
        {
            //TODO:I47 -- Make NotNullIfNullable or Equivalent in Guardian public
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _value = value;
            Exists = true;
        }

        //CN(justification): Suppressing documentation comments on trivially complex methods to prevent useless bloat
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

        public static bool operator !=(Maybe<T> left, Maybe<T> right) => !left.Equals(right);

        public override bool Equals(object? obj)
        {
            if (obj is not Maybe<T> maybe)
                return false;

            return Equals(maybe);
        }

        public bool Equals(Maybe<T> other)
        {
            return other.Exists == Exists
                && EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public static implicit operator Maybe<T>(T value) => new(value);

        public static implicit operator Maybe<T>(None _) => new();

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// NOT SUPPORTED. <see cref="Maybe{T}"/> represents the possibility of a value. Operations
        /// that require this method should only be perfomed on data that definitely exists. Use
        /// <typeparamref name="T"/> directly in these cases.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// <see cref="GetHashCode"/> is called.
        /// </exception>
        public override int GetHashCode() =>
            throw new InvalidOperationException(
                $"Use the {nameof(T)} type directly for data that requires GetHashCode.");
    }
}
