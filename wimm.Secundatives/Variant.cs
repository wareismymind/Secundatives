using System;
using System.Collections.Generic;

namespace wimm.Secundatives
{
    /// <summary>
    /// A typesafe union that contains either a <typeparamref name="T"/> or a <typeparamref name="U"/>
    /// </summary>
    public struct Variant<T, U>
    {
        /// <summary>
        /// Ensures that <typeparamref name="U"/> and <typeparamref name="T"/> are not within the same
        /// inheritance heirarchy
        /// </summary>
        static Variant()
        {
            var uType = typeof(U);
            var tType = typeof(T);

            if (tType.IsSubclassOf(uType) || uType.IsSubclassOf(tType) )
            {
                //TODO:CN -- Maybe fix? Maybe not?
                throw new NotSupportedException(
                    "Cannot create a variant of two classess within the same inheritance heirarchy");
            }
        }

        //Mike Tyson
        private readonly object _value;

        /// <summary>
        /// Checks the type of the type param against the type of the internal member
        /// </summary>
        /// <typeparam name="W"> The type to test for </typeparam>
        /// <returns>Returns true if the internal value is the same as <typeparamref name="W"/></returns>
        /// <remarks> There is no point calling this method with a value of <typeparamref name="W"/> that 
        /// is not either <typeparamref name="T"/> or <typeparamref name="U"/> 
        /// </remarks>
        public bool Is<W>() => _value is W;

        /// <summary>
        /// Gets a value of type <typeparamref name="W"/> if possible. 
        /// </summary>
        /// <typeparam name="W"> The type to attempt to retrieve from the <see cref="Variant{T, U}"/> </typeparam>
        /// <returns> A value of type <typeparamref name="W"/> if it exists </returns>
        /// <exception cref="InvalidOperationException"> The variant's value is not of type 
        /// <typeparamref name="W"/>
        /// </exception>
        /// /// <remarks> There is no point calling this method with a value of <typeparamref name="W"/> that 
        /// is not either <typeparamref name="T"/> or <typeparamref name="U"/> 
        /// </remarks>
        public W Get<W>() => Is<W>() ? (W)_value : throw BadType(typeof(W), _value.GetType());

        /// <summary>
        /// Constructs a <see cref="Variant{T, U}"/> from a value of <typeparamref name="T"/>
        /// </summary>
        /// <param name="value"> The value to store </param>
        public Variant(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Constructs a <see cref="Variant{T, U}"/> from a value of <typeparamref name="U"/>
        /// </summary>
        /// <param name="value"> The value to store </param>
        public Variant(U value)
        {
            _value = value;
        }
        
        public static implicit operator Variant<T, U>(T val) => new Variant<T, U>(val);
        public static implicit operator Variant<T, U>(U val) => new Variant<T, U>(val);

        private static InvalidOperationException BadType(Type expected, Type recieved)
        {
            return new InvalidOperationException(
                $"Variant conversion to uncontained type:\nRequested: {expected}\nGot: {recieved}");
        }
    }


}
