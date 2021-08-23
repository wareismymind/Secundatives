using System.Threading.Tasks;

namespace wimm.Secundatives.Extensions
{
    /// <summary>
    /// Extensions for converting nested, compatible monads into un-nested monads
    /// </summary>
    public static class FlattenExtensions
    {
        /// <summary>
        /// Un-nest a nested result.
        /// </summary>
        /// <typeparam name="TValue"> The value type of the nested <see cref="Result{T, TError}"/></typeparam>
        /// <typeparam name="TErr"> The error type of the nested <see cref="Result{T, TError}"/></typeparam>
        /// <param name="result"> The nested result to be unwrapped and converted to <see cref="Result{TValue, TError}"/></param>
        /// <returns> 
        /// The un-nested result of inspecting the outer result for error and returning it if it exists or the nested result itself otherwise 
        /// </returns>
        public static Result<TValue, TErr> Flatten<TValue, TErr>(this Result<Result<TValue, TErr>, TErr> result)
        {
            return result.IsValue ? result.Value : result.Error;
        }

        /// <summary>
        /// Un-nest a nested result.
        /// </summary>
        /// <typeparam name="TValue"> The value type of the nested <see cref="Result{T, TError}"/></typeparam>
        /// <typeparam name="TErr"> The error type of the nested <see cref="Result{T, TError}"/></typeparam>
        /// <param name="res"> A task that when awaited will be a nested result to be unwrapped and converted to <see cref="Result{TValue, TError}"/></param>
        /// <returns> 
        /// A task containing the un-nested result of inspecting the outer result for error and returning it if it exists or the nested result itself otherwise 
        /// </returns>
        public static async Task<Result<TValue, TErr>> Flatten<TValue, TErr>(this Task<Result<Result<TValue, TErr>, TErr>> res)
        {
            var result = await res;
            return result.IsValue ? result.Value : result.Error;
        }
    }
}
