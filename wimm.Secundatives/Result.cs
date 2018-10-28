using System;
using wimm.Secundatives.Extensions;

namespace wimm.Secundatives
{
    /// <summary>
    /// A basic Result class that handles operations with a possibility of failure
    /// </summary>
    /// <typeparam name="T"> The type to be returned on success </typeparam>
    public class Result<T> : Variant<T,Error>
    {

        /// <summary> A value indicating whether the operation ended in error </summary>
        public bool IsError => Is<Error>();

        /// <summary> A value indicating if the operation successfully returned a value </summary>
        public bool IsValue => Is<T>();

        /// <summary> Gets the value contained in the result. </summary>
        /// <exception cref="InvalidOperationException"> The result contains no value - operation failed </exception>
        public T Value => Get<T>();

        /// <summary> Gets the error contained in the result </summary>
        /// <exception cref="InvalidOperationException"> The result contains no error - operation succeeded </exception>
        public Error Error => Get<Error>();


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

    }

}
