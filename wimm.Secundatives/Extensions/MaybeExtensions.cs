using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wimm.Secundatives.Exceptions;

namespace wimm.Secundatives.Extensions
{
    public static class MaybeExtensions
    {
        public static T Expect<T>(this Maybe<T> maybe)
        {
            return maybe.Expect($"Expected value in {nameof(Maybe<T>)}");
        }

        public static T Expect<T>(this Maybe<T> maybe, string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Parameter must not be whitespace or empty", nameof(message));

            return maybe.Exists ? maybe.Value : throw new ExpectException(message);
        }

        public static T UnwrapOr<T>(this Maybe<T> maybe, T defaultValue)
        {
            return maybe.Exists ? maybe.Value : defaultValue;
        }

        public static T UnwrapOr<T>(this Maybe<T> maybe, Func<T> func)
        {
            return maybe.Exists ? maybe.Value : func();
        }
    }
}
