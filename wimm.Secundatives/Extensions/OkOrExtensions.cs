using System;
using System.Threading.Tasks;
using wimm.Secundatives.Extensions;

namespace wimm.Secundatives
{
    /// <summary>
    /// Methods that convert <see cref="Maybe{T}"/> to <see cref="Result{T, TError}"/> by providing an error value in the case that
    /// the <see cref="Maybe{T}"/> is <see cref="None"/>
    /// </summary>
    public static class OkOrExtensions
    {
        /// <summary>
        /// Maps a <see cref="Maybe{T}"/> into a <see cref="Result{T, TError}"/> by executing <paramref name="func"/> if
        /// <paramref name="value"/> is None. In the case where <paramref name="value"/> contains a value returns a <see cref="Result{T, TError}"/>
        /// containing its value.
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> is a value or an error</param>
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
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> is a value or an error</param>
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
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> is a value or an error</param>
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
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> is a value or an error</param>
        /// <param name="func"> A function returning a <typeparamref name="TError"/> that is executed if <paramref name="value"/> is None</param>
        /// <returns> A <see cref="Result{T, TError}"/> containing the value of <paramref name="value"/> if it contains one, otherwise 
        /// contains the result of executing <paramref name="func"/>
        /// </returns>
        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<T>> value, Func<TError> func)
        {
            var v = await value;
            return v.OkOr(func);

        }


        /// <summary>
        /// Collapsing mapping of a  <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> that may hold a <see cref="Result{T,TErr}"/>
        /// into a <see cref="Result{T, TError}"/> 
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> 
        /// is a the provided result value or a new <see cref="Result{T, TError}"/> created from <paramref name="error"/></param>
        /// <param name="error"> The default value mappted into a <see cref="Result{T, TError}"/> if <paramref name="value"/> is None </param>
        /// <returns> A <see cref="Result{T, TError}"/> either contained by <paramref name="value"/> if it exists or created
        /// from <paramref name="error"/> if it is None</returns>
        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<Result<T, TError>>> value, TError error)
        {
            var res = await value;
            return res.UnwrapOr(error);
        }


        /// <summary>
        /// Collapsing mapping of a  <see cref="Task{T}"/> containing a <see cref="Maybe{T}"/> that may hold a <see cref="Result{T,TErr}"/>
        /// into a <see cref="Result{T, TError}"/> 
        /// </summary>
        /// <typeparam name="T"> The value of the success type </typeparam>
        /// <typeparam name="TError"> The value of the error type created when <paramref name="value"/> contains no value </typeparam>
        /// <param name="value"> The <paramref name="value"/> whose existence determines whether <see cref="Result{T, TError}"/> 
        /// is a the provided result value or a new <see cref="Result{T, TError}"/> created from calling <paramref name="func"/></param>
        /// <param name="func"> A function returning a <typeparamref name="TError"/> that is executed if <paramref name="value"/> is None</param>
        /// <returns> A <see cref="Result{T, TError}"/> either contained by <paramref name="value"/> if it exists or created
        /// from the results of calling <paramref name="func"/> if it is None</returns>
        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<Result<T, TError>>> value, Func<TError> func)
        {
            var res = await value;
            return res.UnwrapOr(func());
        }
    }
}
