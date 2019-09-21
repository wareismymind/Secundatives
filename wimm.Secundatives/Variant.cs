using System;
using System.Collections.Generic;

namespace wimm.Secundatives
{
    /// <summary>
    /// A typesafe union that contains either a <typeparamref name="T"/> or a <typeparamref name="U"/>
    /// </summary>
    public class Variant<T, U>
    {
        /// <summary>
        /// Ensures that <typeparamref name="U"/> and <typeparamref name="T"/> are not within the same
        /// inheritance heirarchy or the same type
        /// </summary>
        static Variant()
        {
            var uType = typeof(U);
            var tType = typeof(T);

            if (tType.IsSubclassOf(uType) || uType.IsSubclassOf(tType))
                //TODO:CN -- Maybe fix? Maybe not?
                throw new NotSupportedException(
                    "Cannot create a variant of two classess within the same inheritance heirarchy");

            if (uType == tType)
                throw new NotSupportedException("Cannot create a variant witih two objects of the same type");
        }

        //Mike Tyson
        private readonly object _value;

        /// <summary>
        /// Checks the type of the type param against the type of the internal member
        /// </summary>
        /// <typeparam name="W"> The type to test for </typeparam>
        /// <returns>Returns true if the internal value is a <typeparamref name="W"/></returns>
        /// <exception cref="NotSupportedException"> 
        /// <typeparamref name="W"/> is not assignable to <typeparamref name="T"/> or <typeparamref name="U"/>
        /// </exception>
        public bool Is<W>()
        {
            var wType = typeof(W);

            if (!IsAssignableFromMemberType(wType))
                throw UnsupportedType(wType, new List<Type> { typeof(T), typeof(U) });

            return _value is W;
        }


        /// <summary>
        /// Gets a value of type <typeparamref name="W"/> if possible. 
        /// </summary>
        /// <typeparam name="W"> The type to attempt to retrieve from the <see cref="Variant{T, U}"/> </typeparam>
        /// <returns> A value of type <typeparamref name="W"/> if it exists </returns>
        /// <exception cref="InvalidOperationException"> The variant's value is not of type 
        /// <typeparamref name="W"/>
        /// </exception>
        /// <exception cref="NotSupportedException"> 
        /// <typeparamref name="W"/> is not assignable from one of <typeparamref name="T"/> or <typeparamref name="U"/>
        /// </exception>
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


        private bool IsAssignableFromMemberType(Type type)
        {
            return typeof(U).IsAssignableFrom(type) || typeof(T).IsAssignableFrom(type);
        }

        private static InvalidOperationException BadType(Type expected, Type recieved)
        {
            return new InvalidOperationException(
                $"Variant conversion to uncontained type:\nRequested: {expected}\nGot: {recieved}");
        }

        private static NotSupportedException UnsupportedType(Type expected, IEnumerable<Type> available)
        {
            return new NotSupportedException(
                $"Variant was checked for type it cannot contain:\nRequested:{expected}\nAvailable: {string.Join("\n", available)}");
        }



    }


}
