using System;

namespace wimm.Secundatives.Extensions
{
    public static class VariantExtensions
    {
        /// <summary>
        /// Maps the contents of a variant to a value of type <typeparamref name="W"/>
        /// </summary>
        /// <typeparam name="T"> <see cref="Variant{T, U}"/> member type </typeparam>
        /// <typeparam name="U"> <see cref="Variant{T, U}"/> member type </typeparam>
        /// <typeparam name="W">
        /// Type that the members of <paramref name="variant"/> will be mapped to 
        /// </typeparam>
        /// <param name="variant"> The <see cref="Variant{T, U}"/> whose types will be mapped </param>
        /// <param name="tMapping"> A function mapping <typeparamref name="T"/> to <typeparamref name="W"/></param>
        /// <param name="uMapping"> A function mapping <typeparamref name="U"/> to <typeparamref name="W"/></param>
        /// <returns> The result of <paramref name="tMapping"/> called with the value of <paramref name="variant"/>
        /// if <paramref name="variant"/> contains a value of type <typeparamref name="T"/> or the result of 
        /// <paramref name="uMapping"/> called with the value of <paramref name="variant"/> if <paramref name="variant"/>
        /// contains a value of type <typeparamref name="U"/>
        /// </returns>
        public static W MapValue<T, U, W>(this Variant<T, U> variant, Func<T, W> tMapping, Func<U, W> uMapping)
            => variant.Is<T>() ? tMapping(variant.Get<T>()) : uMapping(variant.Get<U>());


        /// <summary>
        /// Maps the contents of a variant to an action taking that type. Executes the function matching the type of the value
        /// </summary>
        /// <typeparam name="T"> <see cref="Variant{T, U}"/> member type </typeparam>
        /// <typeparam name="U"> <see cref="Variant{T, U}"/> member type </typeparam>
        /// <param name="variant"> The variant whose value will be mapped </param>
        /// <param name="tAction"> 
        /// The action to be executed with the value of <paramref name="variant"/> if the type of the value
        /// is <typeparamref name="T"/>
        /// </param>
        /// <param name="uAction">
        /// /// The action to be executed with the value of <paramref name="variant"/> if the type of the value
        /// is <typeparamref name="U"/>
        /// </param>
        public static void MapAction<T, U>(this Variant<T, U> variant, Action<T> tAction, Action<U> uAction)
        {
            if (variant.Is<T>())
                tAction(variant.Get<T>());
            else
                uAction(variant.Get<U>());
        }
    }
}
