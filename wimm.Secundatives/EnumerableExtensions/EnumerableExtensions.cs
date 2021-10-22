using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wimm.Secundatives.EnumerableExtensions
{
    /// <summary>
    /// Provides extensions for enumerables of secundative types.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Maybe<U>> Map<T, U>(this IEnumerable<Maybe<T>> target, Func<T, U> func) =>
            target.Select(x => x.Map(func));

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Maybe<U>> Map<T, U>(this IEnumerable<Maybe<T>> target, Func<T, Maybe<U>> func) =>
            target.Select(x => x.Map(func)); // todo: test

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Maybe<T>> target, Func<T, Task<U>> func) =>
            target.Select(x => x.Map(func)); // todo: test

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Maybe<T>> target, Func<T, Task<Maybe<U>>> func) =>
            target.Select(x => x.Map(func)); // todo: test

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Task<Maybe<T>>> target, Func<T, U> func) =>
            target.Select(x => x.Map(func));

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Task<Maybe<T>>> target, Func<T, Maybe<U>> func) =>
            // todo: Use  Map<T, U>(this Task<Maybe<T>>, Func<T, Maybe<U>>) when
            // https://github.com/wareismymind/Secundatives/pull/56/files is merged.
            target.Select(async (task) =>
            {
                var maybe = await task;
                return maybe.Exists
                    ? func(maybe.Value)
                    : Maybe.None;
            });

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Task<Maybe<T>>> target, Func<T, Task<U>> func) =>
            target.Select(x => x.Map(func));

        /// <summary>
        /// Tranforms the elements of the enumerable using the supplied function. Elements that are
        /// <see cref="Maybe.None"/> are unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the source enumerable's values.</typeparam>
        /// <typeparam name="U">The type of the returned enumerable's values.</typeparam>
        /// <param name="target">The enumerable whose value's to transform.</param>
        /// <param name="func">The function to use to transform the enumerable's values.</param>
        /// <returns>An enumerable of the transformed values.</returns>
        public static IEnumerable<Task<Maybe<U>>> Map<T, U>(this IEnumerable<Task<Maybe<T>>> target, Func<T, Task<Maybe<U>>> func) =>
            target.Select(x => x.Map(func));
    }
}
