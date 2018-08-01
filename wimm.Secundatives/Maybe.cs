using System;
using System.Collections.Generic;

namespace wimm.Secundatives
{
    /// <summary>
    /// Minimum implementation for tests to compile.
    /// </summary>
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        public readonly static Maybe<T> None;

        public T Value { get; }

        public Maybe(T value) => throw new NotImplementedException();

        public static bool operator ==(Maybe<T> left, Maybe<T> right) => throw new NotImplementedException();

        public static bool operator !=(Maybe<T> left, Maybe<T> right) => throw new NotImplementedException();

        public override bool Equals(object obj) => throw new NotImplementedException();

        public bool Equals(Maybe<T> other) => throw new NotImplementedException();

        public override int GetHashCode() => throw new NotImplementedException();
    }
}