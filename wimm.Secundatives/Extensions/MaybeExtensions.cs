using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wimm.Secundatives.Exceptions;

namespace wimm.Secundatives.Extensions
{
    /// <summary>
    /// Extensions to make working with <see cref="Maybe{T}"/> simpler and improve ergonomics
    /// </summary>
    public static class MaybeExtensions
    {

        /// <summary>
        /// Extension to indicate that a value missing from a <see cref="Maybe{T}"/> is unrecoverable. 
        /// Unwraps the <see cref="Maybe{T}"/> if it exists or throws a <see cref="ExpectException"/>
        /// </summary>
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybe"/> </typeparam>
        /// <param name="maybe"> The <see cref="Maybe{T}"/> to be inspected </param>
        /// <exception cref="ExpectException"> <paramref name="maybe"/> contains no value </exception>
        /// <returns> The <typeparamref name="T"/> held within <paramref name="maybe"/> if it exists. </returns>
        public static T Expect<T>(this Maybe<T> maybe) => maybe.Expect($"Expected value in {nameof(Maybe<T>)}");


        /// Extension to indicate that a value missing from a <see cref="Maybe{T}"/> is unrecoverable. 
        /// Unwraps the <see cref="Maybe{T}"/> if it exists or throws a <see cref="ExpectException"/>
        /// with a custom message.
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybe"/> </typeparam>
        /// <param name="maybe"> The <see cref="Maybe{T}"/> to be inspected </param>
        /// <param name="message"> The message to be contained in the thrown <see cref="ExpectException"/> 
        /// if <paramref name="maybe"/> contains no value </param>
        /// <exception cref="ExpectException"> <paramref name="maybe"/> contains no value </exception>
        /// <returns> The <typeparamref name="T"/> held within <paramref name="maybe"/> if it exists. </returns>
        public static T Expect<T>(this Maybe<T> maybe, string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Parameter must not be whitespace or empty", nameof(message));

            return maybe.Exists ? maybe.Value : throw new ExpectException(message);
        }


        /// <summary>
        /// Unwraps the <see cref="Maybe{T}"/> if possible and returns a provided default otherwise
        /// </summary>
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybe"/> </typeparam>
        /// <param name="maybe"> The <see cref="Maybe{T}"/> to be unwrapped </param>
        /// <param name="defaultValue"> An instance of <typeparamref name="T"/> to be returned if unwrapping is not 
        /// possible </param>
        /// <returns> The <typeparamref name="T"/> held within <paramref name="maybe"/> if it exists. Otherwise
        /// <paramref name="defaultValue"/> </returns>
        public static T UnwrapOr<T>(this Maybe<T> maybe, T defaultValue) => maybe.Exists ? maybe.Value : defaultValue;


        /// <summary>
        /// Unwraps the <see cref="Maybe{T}"/> if possible and returns a the results of a provided function otherwise
        /// </summary>
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybe"/> </typeparam>
        /// <param name="maybe"> The <see cref="Maybe{T}"/> to be unwrapped </param>
        /// <param name="func"> A function that returns a <typeparamref name="T"/> that will be called if
        /// unwrapping of <paramref name="maybe"/> is not possible </param>
        /// <returns> The <typeparamref name="T"/> held within <paramref name="maybe"/> if it exists. Otherwise
        /// the result of calling <paramref name="func"/> </returns>
        public static T UnwrapOr<T>(this Maybe<T> maybe, Func<T> func) => maybe.Exists ? maybe.Value : func();

    }
}
