using System;
using System.Collections.Generic;
using System.Text;

namespace NepDate
{
    public partial struct NepaliDate
    {
        /// <summary>
        /// Gets the integer representation of this Nepali date in the format "YYYYMMDD".
        /// </summary>
        private int AsInteger
            => int.Parse(string.Concat($"{Year:D4}", $"{Month:D2}", $"{Day:D2}"));

        /// <summary>
        /// Returns a string that represents the current NepaliDate object in the format "yyyy/MM/dd".
        /// </summary>
        /// <returns>A string that represents the current NepaliDate object in the format "yyyy/MM/dd".</returns>
        public override string ToString()
        {
            return $"{Year:D4}/{Month:D2}/{Day:D2}";
        }

        #region Operators

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

        #endregion

        /// <summary>
        /// Determines whether this NepaliDate instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the specified object is a NepaliDate and has the same value as the current instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is NepaliDate date && Equals(date);
        }

        /// <summary>
        /// Determines whether this NepaliDate instance is equal to another NepaliDate instance.
        /// </summary>
        /// <param name="other">The NepaliDate to compare with the current instance.</param>
        /// <returns>true if the specified NepaliDate has the same value as the current instance; otherwise, false.</returns>
        public bool Equals(NepaliDate other)
        {
            return AsInteger == other.AsInteger;
        }

        /// <summary>
        /// Returns the hash code for this NepaliDate instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code based on the value of this NepaliDate instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Year;
                hashCode = (hashCode * 397) ^ Month;
                hashCode = (hashCode * 397) ^ Day;
                return hashCode;
            }
        }

        public int CompareTo(NepaliDate other)
        {
            return other.AsInteger.CompareTo(AsInteger);
        }
    }
}
