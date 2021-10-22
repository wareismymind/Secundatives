using System;
using System.Threading.Tasks;

namespace wimm.Secundatives
{
    /// <summary>
    /// Extensions for mapping <see cref="Maybe{T}"/> to <see cref="Maybe{U}"/>.
    /// </summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose value will be tranformed.</param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// A <see cref="Maybe{U}"/> containing the value returned from <paramref name="func"/>, or
        /// <see cref="Maybe.None"/> if the original <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> target, Func<T, U> func) =>
            target.Exists
                ? func(target.Value)
                : Maybe<U>.None;

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose value will be tranformed.</param>
        /// <param name="func">The function to create the new <see cref="Maybe{U}"/>.</param>
        /// <returns>
        /// The <see cref="Maybe{U}"/> returned from <paramref name="func"/>, or <see cref="Maybe.None"/> if the
        /// original <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> target, Func<T, Maybe<U>> func) =>
            target.Exists
                ? func(target.Value)
                : Maybe<U>.None;

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose value will be tranformed.</param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// A <see cref="Task{MU}"/> whose's result is a <see cref="Maybe{U}"/> containing the result of the
        /// <see cref="Task{U}"/> returned from <paramref name="func"/>, or <see cref="Maybe.None"/> if the original
        /// <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> target, Func<T, Task<U>> func) =>
            target.Exists
                ? await func(target.Value)
                : Maybe<U>.None;

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose value will be tranformed.</param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// The <see cref="Task{MU}"/> returned from <paramref name="func"/>, or <see cref="Maybe.None"/> if the
        /// original <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> target, Func<T, Task<Maybe<U>>> func) =>
            target.Exists
                ? await func(target.Value)
                : Maybe<U>.None;

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">
        /// A <see cref="Task{MT}"/> whose result is the <see cref="Maybe{T}"/> whose value will be tranformed.
        /// </param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// A <see cref="Task{MU}"/> whose result is a <see cref="Maybe{U}"/> containing the value returned from
        /// <paramref name="func"/>, or <see cref="Maybe.None"/> if the original <see cref="Maybe{T}"/> was
        /// <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, U> func)
        {
            var value = await target;
            return value.Map(func);
        }

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">
        /// A <see cref="Task{MT}"/> whose result is the <see cref="Maybe{T}"/> whose value will be tranformed.
        /// </param>
        /// <param name="func">The function to create the new <see cref="Maybe{U}"/>.</param>
        /// <returns>
        /// A <see cref="Task{MU}"/> whose result is the <see cref="Maybe{U}"/> returned from <paramref name="func"/>,
        /// or <see cref="Maybe.None"/> if the original <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Maybe<U>> func)
        {
            var value = await target;
            return value.Map(func);
        }

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">
        /// A <see cref="Task{MT}"/> whose result is the <see cref="Maybe{T}"/> whose value will be tranformed.
        /// </param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// A <see cref="Task{MU}"/> whose's result is a <see cref="Maybe{U}"/> containing the result of the
        /// <see cref="Task{U}"/> returned from <paramref name="func"/>, or <see cref="Maybe.None"/> if the original
        /// <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Task<U>> func)
        {
            var val = await target;
            return await val.Map(func);
        }

        /// <summary>
        /// Creates a new <see cref="Maybe{U}"/> by applying the given function to the existing
        /// <see cref="Maybe{T}"/>'s value. If the existing <see cref="Maybe{T}"/> is <see cref="Maybe.None"/> then the
        /// function is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type of the original value.</typeparam>
        /// <typeparam name="U">The type of the new value.</typeparam>
        /// <param name="target">
        /// A <see cref="Task{MT}"/> whose result is the <see cref="Maybe{T}"/> whose value will be tranformed.
        /// </param>
        /// <param name="func">The function to transform the value.</param>
        /// <returns>
        /// The <see cref="Task{MU}"/> returned from <paramref name="func"/>, or <see cref="Maybe.None"/> if the
        /// original <see cref="Maybe{T}"/> was <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Task<Maybe<U>>> func)
        {
            var val = await target;
            return await val.Map(func);
        }
    }
}
