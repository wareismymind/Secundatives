using System;
using System.Collections.Generic;
using System.Linq;

namespace wimm.Secundatives.ApplyFunctionsExtensions
{
    /// <summary>
    /// Extensions for applying a function to the value of an expression.
    /// </summary>
    public static class ApplyFunctionsExtensions
    {
        /// <summary>
        /// Applies a given function to the value.
        /// </summary>
        /// <typeparam name="TArg">
        /// The type of the value that the function will be applied to (the function's argument).
        /// </typeparam>
        /// <typeparam name="TFunc">The type returned by the function to be applied.</typeparam>
        /// <param name="target">The value to apply <paramref name="fn"/> to.</param>
        /// <param name="fn">The function to apply to <paramref name="target"/>.</param>
        /// <param name="allowNull">
        /// Indicates whether <paramref name="fn"/> should be applied when <paramref name="target"/> is <c>null</c>.
        /// </param>
        /// <returns>The value returned by <paramref name="fn"/> when called with <paramref name="target"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> is <c>null</c> and <paramref name="allowNull"/> is <c>false</c>, or <paramref name="fn"/> is
        /// <c>null</c>.
        /// </exception>
        public static TFunc Apply<TArg, TFunc>(this TArg target, Func<TArg, TFunc> fn, bool allowNull = false)
        {
            if (target == null && !allowNull)
                throw new ArgumentNullException(nameof(target));
            if (fn == null)
                throw new ArgumentNullException(nameof(fn));
            return fn(target);
        }

        /// <summary>
        /// Checks whether the value is in a given enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="target">The value to search <paramref name="enumerable"/> for.</param>
        /// <param name="enumerable">The enumerable to search for <paramref name="target"/> in.</param>
        /// <param name="allowNull">
        /// Indicates whether <paramref name="enumerable"/> should be searched when <paramref name="target"/> is
        /// <c>null</c>.
        /// </param>
        /// <returns>
        /// <c>true</c> if <paramref name="enumerable"/> contains the value of <paramref name="target"/>; otherwise
        /// <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="target"/> is <c>null</c> and <paramref name="allowNull"/> is <c>false</c>, or
        /// <paramref name="enumerable"/> is <c>null</c>.
        /// </exception>
        public static bool In<T>(this T target, IEnumerable<T> enumerable, bool allowNull = false)
        {
            if (target == null && !allowNull)
                throw new ArgumentNullException(nameof(target));
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            return enumerable.Contains(target, EqualityComparer<T>.Default);
        }
    }
}
