using System;
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
        /// <exception cref="ExpectException"> <paramref name="maybe"/> is <see cref="Maybe{T}.None"/> </exception>
        /// <returns> The <typeparamref name="T"/> held within <paramref name="maybe"/> if it exists. </returns>
        public static T Expect<T>(this Maybe<T> maybe) => maybe.Expect($"Expected value in {nameof(Maybe<T>)}");


        /// Extension to indicate that a value missing from a <see cref="Maybe{T}"/> is unrecoverable. 
        /// Unwraps the <see cref="Maybe{T}"/> if it exists or throws a <see cref="ExpectException"/>
        /// with a custom message.
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybe"/> </typeparam>
        /// <param name="maybe"> The <see cref="Maybe{T}"/> to be inspected </param>
        /// <param name="message"> The message to be contained in the thrown <see cref="ExpectException"/> 
        /// if <paramref name="maybe"/> contains no value </param>
        /// <exception cref="ExpectException"> <paramref name="maybe"/> is <see cref="Maybe{T}.None"/></exception>
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
        /// Helper function to apply <see cref="Expect{T}(Maybe{T})"/> in async contexts. Awaits the task containing the <see cref="Maybe{T}"/>
        /// and then applies the <see cref="Expect{T}(Maybe{T})"/> function to it.
        /// </summary>
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybeTask"/> </typeparam>
        /// <param name="maybeTask">A task resulting in a <see cref="Maybe{T}"/> to be inspected</param>
        /// <param name="message"> The message to be contained in the thrown if there is no value <see cref="ExpectException"/> </param> 
        /// <exception cref="ExpectException"> <paramref name="maybeTask"/> results in <see cref="Maybe{T}.None"/></exception>
        /// <returns> The <typeparamref name="T"/> held within the result of <paramref name="maybeTask"/> if it exists</returns>
        public static async Task<T> Expect<T>(this Task<Maybe<T>> maybeTask, string message)
        {
            return (await maybeTask).Expect(message);
        }


        /// <summary>
        /// Helper function to apply <see cref="Expect{T}(Maybe{T})"/> in async contexts. Awaits the task containing the <see cref="Maybe{T}"/>
        /// and then applies the <see cref="Expect{T}(Maybe{T})"/> function to it.
        /// </summary>
        /// <typeparam name="T"> The type of value contained with the <paramref name="maybeTask"/> </typeparam>
        /// <param name="maybeTask">A task resulting in a <see cref="Maybe{T}"/> to be inspected</param>
        /// /// <exception cref="ExpectException"> <paramref name="maybeTask"/> results in <see cref="Maybe{T}.None"/></exception>
        /// <returns> The <typeparamref name="T"/> held within the result of <paramref name="maybeTask"/> if it exists</returns>
        public static async Task<T> Expect<T>(this Task<Maybe<T>> maybeTask)
        {
            return (await maybeTask).Expect();
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


        /// <summary>
        /// An explicit conversion of class types to <see cref="Maybe{T}"/> for ease of use and compatibility
        /// </summary>
        /// <typeparam name="T"> The type that will be tested and the type of the resultant <see cref="Maybe{T}"/> </typeparam>
        /// <param name="value"> The value to be tested </param>
        /// <returns> A <see cref="Maybe{T}"/> with a value if <paramref name="value"/> is not <c> null </c> else 
        /// <see cref="Maybe{T}.None"/>
        /// </returns>
        public static Maybe<T> AsMaybe<T>(this T value) where T : class
        {
            return value != null ? new Maybe<T>(value) : Maybe<T>.None;
        }

        /// <summary>
        /// An explicit conversion from nullable struct types  to <see cref="Maybe{T}"/> for ease of use and compatibility
        /// </summary>
        /// <typeparam name="T"> 
        /// The underlying type of the <see cref="Nullable{T}"/> and the type of the resultant <see cref="Maybe{T}"/>
        /// </typeparam>
        /// <param name="value"> The value to be tested </param>
        /// <returns> A <see cref="Maybe{T}"/> with a value if <paramref name="value"/> is not <c> null </c> else 
        /// <see cref="Maybe{T}.None"/>
        /// </returns>
        public static Maybe<T> AsMaybe<T>(this T? value) where T : struct
        {
            return value.HasValue ? new Maybe<T>(value.Value) : Maybe<T>.None;
        }


        /// <summary>
        /// Helper function that awaits a <see cref="Task{T}"/>'s result and converts it into a <see cref="Maybe{T}"/>
        /// </summary>
        /// <typeparam name="T"> The type that will be returned from awaiting the task and that 
        /// will be tested and the type of the resultant <see cref="Maybe{T}"/> </typeparam>
        /// <param name="value"> A <see cref="Task{T}"/> That will contain the value to be tested </param>
        /// <returns> A <see cref="Maybe{T}"/> with a value if <paramref name="value"/>'s result is not <c> null </c> else 
        /// <see cref="Maybe{T}.None"/>
        /// </returns>
        public static async Task<Maybe<T>> AsMaybe<T>(this Task<T> value) where T : class
        {
            return (await value).AsMaybe();
        }


        /// <summary>
        /// Helper function that awaits a <see cref="Task{T}"/>'s nullable result and converts it into a <see cref="Maybe{T}"/>
        /// </summary>
        /// <typeparam name="T"> 
        /// The underlying type of the <see cref="Nullable{T}"/> and the type of the resultant <see cref="Maybe{T}"/>
        /// </typeparam>
        /// <param name="value"> A <see cref="Task{T}"/> That will contain the value to be tested </param>
        /// <returns> A <see cref="Maybe{T}"/> with a value if <paramref name="value"/>'s result is not <c> null </c> else 
        /// <see cref="Maybe{T}.None"/>
        /// </returns>
        public static async Task<Maybe<T>> AsMaybe<T>(this Task<T?> value) where T : struct
        {
            return (await value).AsMaybe();
        }

        /// <summary>
        /// Builds a chain of <see cref="Maybe{T}"/> expressions that returns the first
        /// <see cref="Maybe{T}"/> that exists.
        /// </summary>
        /// <typeparam name="T">
        /// The value type of of the <see cref="Maybe{T}"/> expressions.
        /// </typeparam>
        /// <param name="primary">The first <see cref="Maybe{T}"/> to inspect for existance.</param>
        /// <param name="alternate">
        /// A <see cref="Func{T}"/> returning the <see cref="Maybe{T}"/> to return if
        /// <paramref name="primary"/> does not exist.
        /// </param>
        /// <returns>
        /// <paramref name="primary"/> if it exists, otherwise the result of evaluating
        /// <paramref name="alternate"/>. NOTE: It's possible that the result of evaluating
        /// <paramref name="alternate"/> will not exist.
        /// </returns>
        public static Maybe<T> Or<T>(this Maybe<T> primary, Func<Maybe<T>> alternate) =>
            primary.Exists ? primary : alternate();

        /// <summary>
        /// Builds a <see cref="Task{T}"/> that executes a chain of <see cref="Maybe{T}"/>
        /// expressions returning the first <see cref="Maybe{T}"/> that exists.
        /// </summary>
        /// <typeparam name="T">
        /// The value type of of the <see cref="Maybe{T}"/> expressions.
        /// </typeparam>
        /// <param name="primary">The first <see cref="Maybe{T}"/> to inspect for existance.</param>
        /// <param name="alternate">
        /// A <see cref="Func{T}"/> returning a <see cref="Task{T}"/> that produces the
        /// <see cref="Maybe{T}"/> to return if <paramref name="primary"/> does not exist.
        /// </param>
        /// <returns><paramref name="primary"/> if it exists, otherwise the result of evaluating
        /// <paramref name="alternate"/>. NOTE: It's possible that the result of evaluating
        /// <paramref name="alternate"/> will not exist.
        /// </returns>
        public static async Task<Maybe<T>> Or<T>(this Maybe<T> primary, Func<Task<Maybe<T>>> alternate) =>
            primary.Exists ? primary : await alternate();

        /// <summary>
        /// Builds a <see cref="Task{T}"/> that executes a chain of <see cref="Maybe{T}"/>
        /// expressions returning the first <see cref="Maybe{T}"/> that exists.
        /// </summary>
        /// <typeparam name="T">
        /// The value type of of the <see cref="Maybe{T}"/> expressions.
        /// </typeparam>
        /// <param name="primary">
        /// A <see cref="Task{T}"/> producing the first <see cref="Maybe{T}"/> to inspect for
        /// existance.
        /// </param>
        /// <param name="alternate">
        /// A <see cref="Func{T}"/> returning the <see cref="Maybe{T}"/> to return if the result of
        /// evaluationg <paramref name="primary"/> does not exist.
        /// </param>
        /// <returns>The result of evaluating <paramref name="primary"/> if it exists, otherwise the
        /// result of evaluating <paramref name="alternate"/>. NOTE: It's possible that the result of
        /// evaluating <paramref name="alternate"/> will not exist.
        /// </returns>
        public static async Task<Maybe<T>> Or<T>(this Task<Maybe<T>> primary, Func<Maybe<T>> alternate) =>
            (await primary).Or(alternate);

        /// <summary>
        /// Builds a <see cref="Task{T}"/> that executes a chain of <see cref="Maybe{T}"/>
        /// expressions returning the first <see cref="Maybe{T}"/> that exists.
        /// </summary>
        /// <typeparam name="T">
        /// The value type of of the <see cref="Maybe{T}"/> expressions.
        /// </typeparam>
        /// <param name="primary">
        /// A <see cref="Task{T}"/> producing the first <see cref="Maybe{T}"/> to inspect for
        /// existance.
        /// </param>
        /// <param name="alternate">
        /// A <see cref="Func{T}"/> returning a <see cref="Task{T}"/> to produce the
        /// <see cref="Maybe{T}"/> to return if the result of evaluationg <paramref name="primary"/> does
        /// not exist.
        /// </param>
        /// <returns>The result of evaluating <paramref name="primary"/> if it exists, otherwise the
        /// result of evaluating <paramref name="alternate"/>. NOTE: It's possible that the result of
        /// evaluating <paramref name="alternate"/> will not exist.
        /// </returns>
        public static async Task<Maybe<T>> Or<T>(this Task<Maybe<T>> primary, Func<Task<Maybe<T>>> alternate) =>
            await (await primary).Or(alternate);
    }
}
