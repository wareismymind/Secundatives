using System;

namespace wimm.Secundatives
{
    /// <summary>
    /// Represents a time of day.
    /// </summary>
    /// <remarks>
    /// This type constrains the value of a <see cref="TimeSpan"/> to a valid time of day. Instances must be cast back
    /// to <see cref="TimeSpan"/> to be used.
    /// </remarks>
    public struct TimeOfDay
    {
        private readonly TimeSpan _time;

        public TimeOfDay(TimeSpan time)
        {
            if (time < TimeSpan.Zero || time >= TimeSpan.FromHours(24))
                throw new ArgumentOutOfRangeException(nameof(time), "Must be a positive value less than 24 hours.");

            _time = time;
        }

        public static implicit operator TimeSpan(TimeOfDay timeOfDay) => timeOfDay._time;
    }
}
