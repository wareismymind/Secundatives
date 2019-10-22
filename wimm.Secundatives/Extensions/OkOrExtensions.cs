using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wimm.Secundatives
{
    public static class OkOrExtensions
    {
        public static Result<T, TError> OkOr<T, TError>(this Maybe<T> value, Func<TError> func)
        {
            if (value.Exists)
                return value.Value;

            return func();
        }

        public static Result<T, TError> OkOr<T, TError>(this Maybe<T> value, TError error)
        {
            if (value.Exists)
                return value.Value;

            return error;
        }

        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<T>> value, TError err)
        {
            var v = await value;
            return v.OkOr(err);
        }

        public static async Task<Result<T, TError>> OkOr<T, TError>(this Task<Maybe<T>> value, Func<TError> func)
        {
            var v = await value;
            return v.OkOr(func);

        }

    }
}
