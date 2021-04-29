using System;
using System.Threading.Tasks;

namespace wimm.Secundatives
{
    public static class ResultMappingExtensions
    {

        /// <summary>
        /// Transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{U, TError}"/> by applying a function if the mapped
        /// <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new <see cref="Result{U, TError}"/> from the 
        /// <typeparamref name="TError"/> contained within  <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by <paramref
        /// name="func"/></typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists</param>
        /// <returns> A <see cref="Result{U, TError}"/> containing the result of calling <paramref name="func"/> on the value of <paramref
        /// name="result"/>
        /// if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the <typeparamref name="TError"/> contained within 
        /// <paramref name="result"/></returns>
        public static Result<U, TError> Map<T, U, TError>(this Result<T, TError> result, Func<T, U> func)
        {
            if (result.IsValue)
                return func(result.Value);

            return result.Error;
        }


        /// <summary>
        /// Transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{U, TError}"/> by applying an async  function if the mapped
        /// <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new <see cref="Result{U, TError}"/> from the 
        /// <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by <paramref name="func"/>
        /// </typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it
        /// exists</param>
        /// <returns> A <see cref="Result{U, TError}"/> containing the result of awaiting a call to  <paramref name="func"/> on the value of
        /// <paramref name="result"/>
        /// if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the <typeparamref name="TError"/> contained within 
        /// <paramref name="result"/></returns>
        public static async Task<Result<U, TError>> Map<T, U, TError>(this Result<T, TError> result, Func<T, Task<U>> func)
        {
            if (result.IsValue)
                return await func(result.Value);

            return result.Error;
        }

        /// <summary>
        /// Asynchronously <see cref="Result{T, TError}"/> into a <see cref="Task{T}"/> <see cref="Result{U, TError}"/> by applying a
        /// function if the mapped  <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new 
        /// <see cref="Result{U,TError}"/> from the <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by 
        /// <paramref name="func"/></typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists</param>
        /// <returns> a <see cref="Result{U, TError}"/> containing the result of awaiting a call to  <paramref name="func"/> on the value of
        /// <paramref name="result"/>if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the 
        /// <typeparamref name="TError"/> contained within <paramref name="result"/></returns>
        public static async Task<Result<U, TError>> Map<T, U, TError>(this Task<Result<T, TError>> result, Func<T, U> func)
        {
            var val = await result;
            return val.Map(func);
        }


        /// <summary>
        /// Asynchronously transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{U, TError}"/> by applying an async function and collapsing
        /// the result if the mapped  <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new 
        /// <see cref="Result{U,TError}"/> from the <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by 
        /// <paramref name="func"/></typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists
        /// into a new <see cref="Result{U, TError}"/></param>
        /// <returns> A <see cref="Result{U, TError}"/> containing the result of awaiting a call to  <paramref name="func"/> on the value 
        /// obtained by awaiting <paramref name="result"/> if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the
        /// <typeparamref name="TError"/> contained within  <paramref name="result"/></returns>
        public static async Task<Result<U, TError>> Map<T, U, TError>(this Task<Result<T, TError>> result, Func<T, Task<U>> func)
        {

            var res = await result;
            return await res.Map(func);
        }


        /// <summary>
        /// Asynchronously transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{U, TError}"/> by applying an async function and collapsing
        /// the result if the mapped  <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new 
        /// <see cref="Result{U,TError}"/> from the <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by 
        /// <paramref name="func"/></typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists
        /// into a new <see cref="Result{U, TError}"/></param>
        /// <returns> A <see cref="Result{U, TError}"/> containing the result of awaiting a call to  <paramref name="func"/> on the value of
        /// <paramref name="result"/>
        /// if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the <typeparamref name="TError"/> contained within 
        /// <paramref name="result"/></returns>
        public static async Task<Result<U, TError>> Map<T, U, TError>(this Result<T, TError> result, Func<T, Task<Result<U, TError>>> func)
        {
            if (result.IsValue)
                return await func(result.Value);

            return result.Error;
        }


        /// <summary>
        /// Asynchronously transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{U, TError}"/> by applying an async function and collapsing
        /// the result if the mapped  <see cref="Result{T, TError}"/> containts a value otherwise by constructing a new 
        /// <see cref="Result{U,TError}"/> from the <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value will be transformed to by 
        /// <paramref name="func"/></typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The async function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists
        /// into a new <see cref="Result{U, TError}"/></param>
        /// <returns> A <see cref="Result{U, TError}"/> containing the result of awaiting a call to  <paramref name="func"/> on the value of
        /// <paramref name="result"/>
        /// if it exists. Otherwise returns a <see cref="Result{U, TError}"/> containing the <typeparamref name="TError"/> contained within 
        /// <paramref name="result"/></returns>
        public static async Task<Result<U, TError>> Map<T, U, TError>(this Task<Result<T, TError>> result, Func<T, Task<Result<U, TError>>> func)
        {
            var res = await result;
            if (res.IsValue)
                return await func(res.Value);

            return res.Error;
        }


        /// <summary>
        /// Transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{T, UError}"/> by applying a function if the mapped 
        /// <see cref="Result{T, TError}"/> contains a <typeparamref name="UError"/>. Otherwise constructs a new 
        /// <see cref="Result{T, UError}"/> from the value contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="TError"> The error type of the initial result that will be transformed </typeparam>
        /// <typeparam name="UError"> The type returned by applying <paramref name="func"/> to the <typeparamref name="TError"/> 
        /// contained within <paramref name="result"/></typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="result"/>'s <typeparamref name="TError"/> if it exists
        /// </param>
        /// <returns> A <see cref="Result{T, UError}"/> containing the result of calling <paramref name="func"/> on the 
        /// <typeparamref name="TError"/> of <paramref name="result"/> if it exists. Otherwise returns a <see cref="Result{T, UError}"/> 
        /// containing the <typeparamref name="T"/> contained within <paramref name="result"/></returns>
        public static Result<T, UError> MapError<T, TError, UError>(this Result<T, TError> result, Func<TError, UError> func)
        {
            if (result.IsError)
                return func(result.Error);

            return result.Value;
        }


        /// <summary>
        /// Asynchronously transforms a <see cref="Result{T, TError}"/> into a <see cref="Result{T, UError}"/> by applying a function if the mapped 
        /// <see cref="Result{T, TError}"/> contains a <typeparamref name="UError"/>. Otherwise constructs a new 
        /// <see cref="Result{T, UError}"/> from the value contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="TError"> The error type of the initial result that will be transformed </typeparam>
        /// <typeparam name="UError"> The type returned by applying <paramref name="func"/> to the <typeparamref name="TError"/> 
        /// contained within <paramref name="result"/></typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines if a transform occurs </param>
        /// <param name="func"> The function that will transform <paramref name="result"/>'s <typeparamref name="TError"/> if it exists
        /// </param>
        /// <returns> A <see cref="Result{T, UError}"/> containing the result of calling <paramref name="func"/> on the 
        /// <typeparamref name="TError"/> of <paramref name="result"/> if it exists. Otherwise returns a <see cref="Result{T, UError}"/> 
        /// containing the <typeparamref name="T"/> contained within <paramref name="result"/></returns>
        public static async Task<Result<T, UError>> MapError<T, TError, UError>(this Task<Result<T, TError>> result, Func<TError, UError> func)
        {
            var val = await result;
            return val.MapError(func);
        }

        /// <summary>
        /// Transforms a <see cref="Result{T, TError}"/> into a <typeparamref name="U"/> by applying a function if the mapped
        /// <see cref="Result{T, TError}"/> containts a value, otherwise by applying a function to the 
        /// <typeparamref name="TError"/> contained within <paramref name="result"/>
        /// </summary>
        /// <typeparam name="T"> The success type of the initial result </typeparam>
        /// <typeparam name="U"> The type that <paramref name="result"/>'s value and error are transformed to by
        /// <paramref name="valueFunc"/> and  <paramref name="errorFunc"/> </typeparam>
        /// <typeparam name="TError"> The error type of the results</typeparam>
        /// <param name="result"> A <see cref="Result{T, TError}"/> whose state determines which function is applied </param>
        /// <param name="valueFunc"> The function that will transform <paramref name="result"/>'s <typeparamref name="T"/> if it exists</param>
        /// <param name="errorFunc"> The function that will transform <paramref name="result"/>'s <typeparamref name="TError"/> if it exists</param>
        /// <returns> A <typeparamref name="U"/> that is returned by calling <paramref name="valueFunc"/> on the value of <paramref
        /// name="result"/> or by calling <paramref name="errorFunc"/> on the error from <paramref name="result"/>
        /// </returns>
        public static U MapOr<T, U, TError>(this Result<T, TError> result, Func<T, U> valueFunc, Func<TError, U> errorFunc)
        {
            return result.IsValue ? valueFunc(result.Value) : errorFunc(result.Error);
        }
    }
}
