using System;
using wimm.Secundatives.Extensions;

namespace wimm.Secundatives
{
    /// <summary>
    /// A result class that handles operations with a possibility of failure need to be able to report the error context
    /// with a custom error type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class Result<T,TError> : Variant<T, TError>
    {
        /// <summary> A value indicating whether the operation resulted in an error type </summary>
        public bool IsError => Is<TError>();

        /// <summary> A value indicating if the operation successfully returned a value </summary>
        public bool IsValue => Is<T>();


        /// <summary> Gets the value contained in the result. </summary>
        /// <exception cref="InvalidOperationException"> The result contains no value - operation failed </exception>
        public T Value => Get<T>();

        /// <summary> Gets the error contained in the result </summary>
        /// <exception cref="InvalidOperationException"> The result contains no error - operation succeeded </exception>
        public TError Error => Get<TError>();

        /// <summary> Gets the optional value as a <see cref="Maybe{T}"/> </summary>
        public Maybe<T> Ok() => IsValue ? new Maybe<T>(Value) : new Maybe<T>();

        /// <summary> Gets the optional error as a <see cref="Maybe{TError}"/> </summary>
        public Maybe<TError> Err() => IsError ? new Maybe<TError>(Error) : new Maybe<TError>();

        /// <summary>
        /// Constructs a <see cref="Result{T}"/> from a <typeparamref name="T"/> value
        /// </summary>
        /// <param name="value"> A <typeparamref name="T"/> value of the result, indicates success </param>
        public Result(T value) : base(value)
        {
        }

        /// <summary> Constructs a <see cref="Result{T}"/> from an <see cref="Error"/> </summary>
        /// <param name="err"> The <see cref="TError"/> explaining why the operation was unsucessful </param>
        public Result(TError err) : base(err)
        {
        }

        public static implicit operator Result<T,TError>(T value) => new Result<T,TError>(value);
        public static implicit operator Result<T, TError>(TError error) => new Result<T, TError>(error);
    }



    /// <summary>
    /// A basic Result class that handles operations with a possibility of failure that uses a special error class to 
    /// report failure
    /// </summary>
    /// <typeparam name="T"> The type to be returned on success </typeparam>
    public class Result<T> : Result<T,Error>
    {

        /// <summary>
        /// Constructs a <see cref="Result{T}"/> from a <typeparamref name="T"/> value
        /// </summary>
        /// <param name="value"> A <typeparamref name="T"/> value of the result, indicates success </param>
        public Result(T value) : base(value)
        {
        }

        /// <summary> Constructs a <see cref="Result{T}"/> from an <see cref="Error"/> </summary>
        /// <param name="err"> The <see cref="Error"/> explaining why the operation was unsucessful </param>
        public Result(Error err) : base(err)
        {
        }

        public static implicit operator Result<T>(T value) => new Result<T>(value);
        public static implicit operator Result<T>(Error error) => new Result<T>(error);

    }

}
