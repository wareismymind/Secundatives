using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// A basic Result class that handles operations with a possibility of failure
    /// </summary>
    /// <typeparam name="T"> The type to be returned on success </typeparam>
    public struct Result<T>
    {
        private readonly Variant<T, Error> _value;

        /// <summary> A value indicating whether the operation ended in error </summary>
        public bool IsError => _value.Is<Error>();

        /// <summary> A value indicating if the operation successfully returned a value </summary>
        public bool IsSome => _value.Is<T>();

        /// <summary> Gets the value contained in the result. </summary>
        /// <exception cref="InvalidOperationException"> The result contains no value - operation failed </exception>
        public T Value => _value.Get<T>();

        /// <summary> Gets the error contained in the result </summary>
        /// <exception cref="InvalidOperationException"> The result contains no error - operation succeeded </exception>
        public Error Error => _value.Get<Error>();


        /// <summary>
        /// Constructs a <see cref="Result{T}"/> from a <see cref="Variant{T, U}"/> containing a 
        /// <typeparamref name="T"/> or an <see cref="Error"/>
        /// </summary>
        /// <param name="variant">A valid <see cref="Variant{T, Error}"/>  </param>
        public Result(Variant<T, Error> variant)
        {
            _value = variant;
        }

        /// <summary>
        /// Constructs a <see cref="Result{T}"/> from a <typeparamref name="T"/> value
        /// </summary>
        /// <param name="value"> A <typeparamref name="T"/> value of the result, indicates success </param>
        public Result(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Constructs a <see cref="Result{T}"/> from an <see cref="Error"/>
        /// </summary>
        /// <param name="err"> The <see cref="Error"/> explaining why the operation was unsucessful </param>
        public Result(Error err)
        {
            _value = err;
        }

        

    }

}
