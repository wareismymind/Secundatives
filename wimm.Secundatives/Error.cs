using System;
using System.Diagnostics;

namespace wimm.Secundatives
{
    /// <summary>
    /// Base class for Error types to be returned from results. 
    /// </summary>
    [DebuggerDisplay("{Message}")]
    public class Error
    {
        /// <summary>
        /// Constructs a new instance of <see cref="Error"/> with the specified message
        /// </summary>
        /// <param name="message"> The message to be included with the error </param>
        /// <exception cref="ArgumentNullException"> <paramref name="message"/> is null </exception>
        /// <exception cref="ArgumentException"> <paramref name="message"/> is whitespace </exception>
        public Error(string message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException(
                    "Error message must contain one or more non-whitespace characters", nameof(message));

            Message = message;
        }

        /// <summary>
        /// A detailed, non-null message describing the error. 
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Converts the <see cref="Error"/> into a <see cref="string"/>. 
        /// </summary>
        /// <returns> The formatted error message </returns>
        public override sealed string ToString()
        {
            return Message;
        }
    }
}
