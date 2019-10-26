using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wimm.Secundatives
{
    public static class OkOrExtensions
    {

        /// <summary>
        /// Maps a <see cref="Maybe{T}"/> into a <see cref="Result{T, TError}"/> by executing <paramref name="func"/> if
        /// <paramref name="value"/> is None. In the case where <paramref name="value"/> contains a value returns a <see cref="Result{T, TError}"/>
        /// containing its value.
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> </param>
        /// <param name="func"> A function returning a <typeparamref name="TError"/> that is executed if <paramref name="value"/> is None</param>
        /// <returns> A <see cref="Result{T, TError}"/> containing the value of <paramref name="value"/> if it contains one, otherwise 
        /// contains the result of executing <paramref name="func"/>
        /// </returns>
        public static Result<T, TError> OkOr<T, TError>(this Maybe<T> value, Func<TError> func)
        {
            if (value.Exists)
                return value.Value;

            return func();
        }


        /// <summary>
        /// Maps a <see cref="Maybe{T}"/> into a <see cref="Result{T, TError}"/> by returning a default value <paramref name="error"/> if
        /// <paramref name="value"/> is None. In the case where <paramref name="value"/> contains a value returns a <see cref="Result{T, TError}"/>
        /// containing its value.
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> </param>
        /// <param name="error"> The default value returned if <paramref name="value"/> is None </param>
        /// <returns> A <see cref="Result{T, TError}"/> containing the value of <paramref name="value"/> if it contains one, otherwise 
        /// contains <paramref name="error"/>
        /// </returns>
        public static Result<T, TError> OkOr<T, TError>(this Maybe<T> value, TError error)
        {
            if (value.Exists)
                return value.Value;

            return error;
        }


        /// <summary>
        /// Maps a <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> into a <see cref="Result{T, TError}"/> by returning a default value <paramref name="error"/> if
        /// <paramref name="value"/> is None. In the case where <paramref name="value"/> contains a value returns a <see cref="Result{T, TError}"/>
        /// containing its value.
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> </param>
        /// <param name="error"> The default value returned if <paramref name="value"/> is None </param>
        /// <returns> A <see cref="Result{T, TError}"/> containing the value of <paramref name="value"/> if it contains one, otherwise 
        /// contains <paramref name="error"/>
        /// </returns>
        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<T>> value, TError error)
        {
            var v = await value;
            return v.OkOr(error);
        }

        /// <summary>
        /// Maps a <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> into a <see cref="Result{T, TError}"/> by executing <paramref name="func"/> if
        /// <paramref name="value"/> is None. In the case where <paramref name="value"/> contains a value returns a <see cref="Result{T, TError}"/>
        /// containing its value.
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> </param>
        /// <param name="func"> A function returning a <typeparamref name="TError"/> that is executed if <paramref name="value"/> is None</param>
        /// <returns> A <see cref="Result{T, TError}"/> containing the value of <paramref name="value"/> if it contains one, otherwise 
        /// contains the result of executing <paramref name="func"/>
        /// </returns>
        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<T>> value, Func<TError> func)
        {
            var v = await value;
            return v.OkOr(func);

        }

    }
}
