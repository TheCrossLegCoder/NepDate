using System;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        /// <summary>
        /// Subtracts two NepaliDate objects and returns a TimeSpan object representing the difference.
        /// </summary>
        /// <param name="d1">The first NepaliDate object to subtract.</param>
        /// <param name="d2">The second NepaliDate object to subtract.</param>
        /// <returns>A TimeSpan object representing the difference between d1 and d2.</returns>
        public static TimeSpan operator -(NepaliDate d1, NepaliDate d2)
        {
            return d1.EnglishDate.Date.Subtract(d2.EnglishDate.Date);
        }

        /// <summary>
        /// Determines whether two NepaliDate objects represent the same date.
        /// </summary>
        /// <param name="d1">The first NepaliDate object to compare.</param>
        /// <param name="d2">The second NepaliDate object to compare.</param>
        /// <returns>true if d1 and d2 represent the same date; otherwise, false.</returns>
        public static bool operator ==(NepaliDate d1, NepaliDate d2)
        {
            return d1.AsInteger == d2.AsInteger;
        }

        /// <summary>
        /// Determines whether two NepaliDate objects represent different dates.
        /// </summary>
        /// <param name="d1">The first NepaliDate object to compare.</param>
        /// <param name="d2">The second NepaliDate object to compare.</param>
        /// <returns>true if d1 and d2 represent different dates; otherwise, false.</returns>
        public static bool operator !=(NepaliDate d1, NepaliDate d2)
        {
            return d1.AsInteger != d2.AsInteger;
        }

        /// <summary>
        /// Determines whether one NepaliDate value is less than another NepaliDate value.
        /// </summary>
        /// <param name="t1">The first NepaliDate to compare.</param>
        /// <param name="t2">The second NepaliDate to compare.</param>
        /// <returns>true if t1 is less than t2; otherwise, false.</returns>
        public static bool operator <(NepaliDate t1, NepaliDate t2)
        {
            return t1.AsInteger < t2.AsInteger;
        }

        /// <summary>
        /// Determines whether one NepaliDate value is less than or equal to another NepaliDate value.
        /// </summary>
        /// <param name="t1">The first NepaliDate to compare.</param>
        /// <param name="t2">The second NepaliDate to compare.</param>
        /// <returns>true if t1 is less than or equal to t2; otherwise, false.</returns>
        public static bool operator <=(NepaliDate t1, NepaliDate t2)
        {
            return t1.AsInteger <= t2.AsInteger;
        }

        /// <summary>
        /// Determines whether one NepaliDate value is greater than another NepaliDate value.
        /// </summary>
        /// <param name="t1">The first NepaliDate to compare.</param>
        /// <param name="t2">The second NepaliDate to compare.</param>
        /// <returns>true if t1 is greater than t2; otherwise, false.</returns>
        public static bool operator >(NepaliDate t1, NepaliDate t2)
        {
            return t1.AsInteger > t2.AsInteger;
        }

        /// <summary>
        /// Determines whether one NepaliDate value is greater than or equal to another NepaliDate value.
        /// </summary>
        /// <param name="t1">The first NepaliDate to compare.</param>
        /// <param name="t2">The second NepaliDate to compare.</param>
        /// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
        public static bool operator >=(NepaliDate t1, NepaliDate t2)
        {
            return t1.AsInteger >= t2.AsInteger;
        }
    }
}
