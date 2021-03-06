﻿using System;
using System.Threading.Tasks;

namespace wimm.Secundatives
{
    /// <summary> Extensions for mapping <see cref="Maybe{T}"/> to <see cref="Maybe{U}"/></summary>
    public static class MappingExtensions
    {
        /// <summary>
        /// Transforms a <see cref="Maybe{T}"/> into a <see cref="Maybe{U}"/> by applying a function in the case that <paramref name="value"/>
        /// contains a value or <see cref="Maybe{U}.None"/> if <paramref name="value"/> is <see cref="Maybe{T}.None"/>
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="value"> A <see cref="Maybe{T}"/> that determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="value"/>'s T</param>
        /// <returns> A <see cref="Maybe{U}"/> equal to <see cref="Maybe{U}.None"/> if <paramref name="value"/> is none otherwise a 
        /// <see cref="Maybe{T}"/> containing the result of <paramref name="func"/> being applied to <paramref name="value"/>'s 
        /// <typeparamref name="T"/>
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> value, Func<T, U> func)
        {
            return value.Exists
                ? func(value.Value)
                : Maybe<U>.None;
        }


        /// <summary>
        /// Transforms a <see cref="Maybe{T}"/> into a <see cref="Maybe{U}"/> by applying a function in the case that <paramref name="value"/>
        /// contains a value or <see cref="Maybe{U}.None"/> if <paramref name="value"/> is <see cref="Maybe{T}.None"/>
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="value"> A <see cref="Maybe{T}"/> that determines if a transform occurs </param>
        /// <param name="func"> A function that produces the resultant <see cref="Maybe{T}"/></param>
        /// <returns> A <see cref="Maybe{U}"/> equal to <see cref="Maybe{U}.None"/> if <paramref name="value"/> is none
        /// otherwise the result of <paramref name="func"/> being applied to <paramref name="value"/>'s <typeparamref name="T"/>
        /// </returns>
        public static Maybe<U> Map<T, U>(this Maybe<T> value, Func<T, Maybe<U>> func)
        {
            return value.Exists
                ? func(value.Value)
                : Maybe<U>.None;
        }


        /// <summary>
        /// Transforms a <see cref="Maybe{T}"/> into a <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> by applying an async function 
        /// in the case that <paramref name="value"/> contains a value or <see cref="Maybe{U}.None"/> if <paramref name="value"/> is <see cref="Maybe{T}.None"/>
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="value"> A <see cref="Maybe{T}"/> that determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="value"/>'s T</param>
        /// <returns> A <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> that when awaited will be equal to <see cref="Maybe{U}.None"/> 
        /// if <paramref name="value"/> is none otherwise the result of <paramref name="func"/> being applied to <paramref name="value"/>'s <typeparamref name="T"/>
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> value, Func<T, Task<U>> func)
        {
            return value.Exists
                ? await func(value.Value)
                : Maybe<U>.None;
        }


        /// <summary>
        /// Transforms a <see cref="Maybe{T}"/> into a <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> by flattening the return value 
        /// of an async function applied to the value contained in <paramref name="value"/> in the case that <paramref name="value"/> contains a 
        /// value or <see cref="Maybe{U}.None"/> 
        /// if <paramref name="value"/> is <see cref="Maybe{T}.None"/> </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="value"> A <see cref="Maybe{T}"/> that determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="value"/>'s T into a <see cref="Maybe{U}"/></param>
        /// <returns> A <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> that when awaited will be equal to <see cref="Maybe{U}.None"/> 
        /// if <paramref name="value"/> is none otherwise the result of <paramref name="func"/> being applied to <paramref name="value"/>'s 
        /// <typeparamref name="T"/>
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Maybe<T> value, Func<T, Task<Maybe<U>>> func)
        {
            return value.Exists
                ? await func(value.Value)
                : Maybe<U>.None;
        }

        /// <summary>
        /// Transforms a <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> into a <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> 
        /// by applying a function in the case that <paramref name="task"/> when awaited, contains a value or <see cref="Maybe{U}.None"/> if 
        /// <paramref name="task"/>'s results in <see cref="Maybe{T}.None"/>
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="task"> A <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> that represents a future value of <typeparamref name="T"/>
        /// that when awaited determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="task"/>'s T</param>
        /// <returns> A <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> that when awaited will be equal to <see cref="Maybe{U}.None"/> 
        /// if <paramref name="task"/> is none otherwise the result of <paramref name="func"/> being applied to <paramref name="task"/>'s 
        /// <typeparamref name="T"/>
        /// </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> task, Func<T, U> func)
        {
            var value = await task;
            return value.Map(func);
        }


        /// <summary>
        /// Transforms a Task containing a <see cref="Maybe{T}"/> by applying a function <paramref name="func"/> if
        /// the value of <paramref name="task"/> exists. Otherwise returns <see cref="Maybe{U}.None"/> 
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="task"> A <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> that represents a future value of <typeparamref name="T"/>
        /// that when awaited determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="task"/>'s T into a <see cref="Maybe{U}"/></param>
        /// <returns> A <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> that when awaited will be equal to <see cref="Maybe{U}.None"/> 
        /// if <paramref name="task"/> is none otherwise the result of <paramref name="func"/> being applied to <paramref name="task"/>'s 
        /// <typeparamref name="T"/> </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> task, Func<T, Task<U>> func)
        {
            var val = await task;
            return await val.Map(func);
        }


        /// <summary>
        /// Transforms a Task containing a <see cref="Maybe{T}"/> by applying a function <paramref name="func"/> that returns a maybe. If
        /// the value of <paramref name="task"/> exists the function is applied and the <see cref="Maybe{T}"/> generated by the function is returned
        /// otherwise returns <see cref="Maybe{U}.None"/> 
        /// </summary>
        /// <typeparam name="T"> The type to be mapped from</typeparam>
        /// <typeparam name="U"> The type to be mapped to </typeparam>
        /// <param name="task"> A <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> that represents a future value of <typeparamref name="T"/>
        /// that when awaited determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="task"/>'s T into a <see cref="Maybe{U}"/></param>
        /// <returns> A <see cref="Task{T}"/> containing a <see cref="Maybe{U}"/> that when awaited will be equal to <see cref="Maybe{U}.None"/> 
        /// if <paramref name="task"/> is none otherwise the result of <paramref name="func"/> being applied to <paramref name="task"/>'s 
        /// <typeparamref name="T"/> </returns>
        public static async Task<Maybe<U>> Map<T, U>(this Task<Maybe<T>> task, Func<T, Task<Maybe<U>>> func)
        {
            var val = await task;
            return await val.Map(func);
        }
    }
}
