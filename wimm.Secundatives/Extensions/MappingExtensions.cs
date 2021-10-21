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
        /// Transforms <paramref name="target"/> into a <see cref="Maybe{U}"/> by applying <paramref name="func"/> to
        /// its <see cref="Maybe{T}.Value"/>. If <paramref name="target"/> is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to transform.</param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Maybe{U}"/> containing the value returned by <paramref name="func"/>, or
        /// <see cref="Maybe.None"/> if <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> target, Func<T, U> func) =>
            target.Exists
                ? func(target.Value)
                : Maybe<U>.None;


        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Maybe{U}"/> by applying <paramref name="func"/> to
        /// its <see cref="Maybe{T}.Value"/>. If <paramref name="target"/> is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to transform.</param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Maybe{U}"/> returned by <paramref name="func"/>, or <see cref="Maybe.None"/> if
        /// <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> target, Func<T, Maybe<U>> func) =>
            target.Exists
                ? func(target.Value)
                : Maybe<U>.None;


        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to its <see cref="Maybe{T}.Value"/>. If <paramref name="target"/> is
        /// <see cref="Maybe.None"/> then <paramref name="func"/> is not applied and a <see cref="Task{M}"/> containing
        /// <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to transform.</param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> containing the <see cref="Task{U}.Result"/> of
        /// the <see cref="Task{U}"/> returned by <paramref name="func"/>, or a <see cref="Task{MU}"/> containing
        /// <see cref="Maybe.None"/> if <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> target, Func<T, Task<U>> func) =>
            target.Exists
                ? await func(target.Value)
                : Maybe<U>.None;


        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to its <see cref="Maybe{T}.Value"/>. If <paramref name="target"/> is
        /// <see cref="Maybe.None"/> then <paramref name="func"/> is not applied and a <see cref="Task{M}"/> containing
        /// <see cref="Maybe.None"/> is returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">The <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to transform.</param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Task{MU}"/> returned by <paramref name="func"/>, or a <see cref="Task{MU}"/> containing
        /// <see cref="Maybe.None"/> if <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> target, Func<T, Task<Maybe<U>>> func) =>
            target.Exists
                ? await func(target.Value)
                : Maybe<U>.None;

        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to the <see cref="Maybe{T}.Value"/> of its <see cref="Task{MT}.Result"/>.
        /// If the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and a <see cref="Task{MU}"/> containing <see cref="Maybe.None"/> is
        /// returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">
        /// The <see cref="Task{MT}"/> containing the <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to
        /// transform.
        /// </param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of the <see cref="Task{MT}.Result"/> of
        /// <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> containing the value returned by
        /// <paramref name="func"/>, or a <see cref="Task{MU}"/> containing <see cref="Maybe.None"/> if
        /// the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, U> func)
        {
            var value = await target;
            return value.Map(func);
        }


        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to the <see cref="Maybe{T}.Value"/> of its <see cref="Task{MT}.Result"/>.
        /// If the <see cref="Task{MT}.Result"/> of <paramref name="target"/>  is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and a <see cref="Task{MU}"/> containing <see cref="Maybe.None"/> is
        /// returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">
        /// The <see cref="Task{MT}"/> containing the <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to
        /// transform.
        /// </param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of the <see cref="Task{MT}.Result"/> of
        /// <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{MU}"/> containing the <see cref="Maybe{U}"/> returned by <paramref name="func"/>, or a
        /// <see cref="Task{MU}"/> containing <see cref="Maybe.None"/> if the <see cref="Task{MT}.Result"/> of
        /// <paramref name="target"/> is <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Maybe<U>> func)
        {
            var value = await target;
            return value.Map(func);
        }


        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to the <see cref="Maybe{T}.Value"/> of its <see cref="Task{MT}.Result"/>.
        /// If the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and a <see cref="Task{M}"/> containing <see cref="Maybe.None"/> is
        /// returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">
        /// The <see cref="Task{MT}"/> containing the <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to
        /// transform.
        /// </param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of the <see cref="Task{MT}.Result"/> of
        /// <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> containing the <see cref="Task{U}.Result"/> of
        /// the <see cref="Task{U}"/> returned by <paramref name="func"/>, or a <see cref="Task{MU}"/> containing
        /// <see cref="Maybe.None"/> if the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is
        /// <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Task<U>> func)
        {
            var val = await target;
            return await val.Map(func);
        }

        /// <summary>
        /// Transforms <paramref name="target"/> into a <see cref="Task{MU}"/> containing a <see cref="Maybe{U}"/> by
        /// applying <paramref name="func"/> to the <see cref="Maybe{T}.Value"/> of its <see cref="Task{MT}.Result"/>.
        /// If the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is <see cref="Maybe.None"/> then
        /// <paramref name="func"/> is not applied and a <see cref="Task{MU}"/> containing <see cref="Maybe.None"/> is
        /// returned directly.
        /// </summary>
        /// <typeparam name="T">The type to be mapped from.</typeparam>
        /// <typeparam name="U">The type to be mapped to.</typeparam>
        /// <param name="target">
        /// The <see cref="Task{MT}"/> containing the <see cref="Maybe{T}"/> whose <see cref="Maybe{T}.Value"/> to
        /// transform.
        /// </param>
        /// <param name="func">
        /// The function to use to transform the <see cref="Maybe{T}.Value"/> of the <see cref="Task{MT}.Result"/> of
        /// <paramref name="target"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Task{MU}"/> returned by <paramref name="func"/>, or a <see cref="Task{MU}"/> containing
        /// <see cref="Maybe.None"/> if the <see cref="Task{MT}.Result"/> of <paramref name="target"/> is
        /// <see cref="Maybe.None"/>.
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> target, Func<T, Task<Maybe<U>>> func)
        {
            var val = await target;
            return await val.Map(func);
        }
    }
}
