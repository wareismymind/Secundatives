using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// Represents a time within the day as a <see cref="TimeSpan"/> between 00:00:00 and 23:59:59
    /// that corresponds to the offset into the day.
    /// </summary>
    /// <remarks>
    /// This type constrains the value of a <see cref="TimeSpan"/> to a valid time of day. Instances must be cast back
    /// to <see cref="TimeSpan"/> to be used.
    /// </remarks>
    public struct TimeOfDay
    {
        private readonly TimeSpan _time;

        /// <summary>
        /// Constructs a new <see cref="TimeOfDay"/> from <paramref name="time"/>.
        /// </summary>
        /// <param name="time">The time of day.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="time"/> is negative or exceeds the length of a day.
        /// </exception>
        public TimeOfDay(TimeSpan time)
        {
            if (time < TimeSpan.Zero || time >= TimeSpan.FromHours(24))
                throw new ArgumentOutOfRangeException(nameof(time), "Must be a positive value less than 24 hours.");

            _time = time;
        }

        /// <summary>
        /// Converting constructor that creates a timespan from a time of day
        /// </summary>
        /// <param name="timeOfDay"> The time of day to be converted </param>
        public static implicit operator TimeSpan(TimeOfDay timeOfDay) => timeOfDay._time;
    }
}
