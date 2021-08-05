using System;
using wimm.Secundatives.Extensions;

namespace wimm.Secundatives.Exceptions
{
    /// <summary>
    /// Exception thrown when <see cref="MaybeExtensions.Expect{T}(Maybe{T})"/> or one of its overrides are called
    /// on an empty <see cref="Maybe{T}"/>
    /// </summary>
    public class ExpectException : Exception
    {
        /// <summary>
        /// Constructs an <see cref="ExpectException"/> with the given message
        /// </summary>
        /// <param name="message"></param>
        public ExpectException(string message) : base(message)
        {
        }
    }
}
