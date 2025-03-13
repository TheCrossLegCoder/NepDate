using System;

namespace NepDate.Extensions
{
    /// <summary>
    /// Provides extension methods for common date conversion operations between calendar systems.
    /// </summary>
    /// <remarks>
    /// These extension methods make it easy to convert between DateTime (Gregorian calendar)
    /// and NepaliDate (Bikram Sambat calendar) objects using a fluent, natural syntax.
    /// </remarks>
    public static class Extensions
    {
        /// <summary>
        /// Converts a DateTime object (Gregorian calendar) to its equivalent NepaliDate (Bikram Sambat calendar).
        /// </summary>
        /// <param name="englishDate">The DateTime object to convert.</param>
        /// <returns>A NepaliDate object representing the equivalent date in the Bikram Sambat calendar.</returns>
        /// <remarks>
        /// This extension method allows for natural and readable syntax when converting dates:
        /// <code>
        /// DateTime today = DateTime.Now;
        /// NepaliDate nepaliToday = today.ToNepaliDate();
        /// </code>
        /// 
        /// Only the date portion (year, month, day) of the DateTime is used in the conversion.
        /// The time portion is ignored, as NepaliDate only represents date values, not time.
        /// 
        /// For converting multiple dates efficiently, consider using the BulkConvert class instead.
        /// </remarks>
        /// <exception cref="NepDate.Exceptions.NepDateException.InvalidNepaliDateFormatException">
        /// Thrown if the conversion results in a date outside the supported range (1901-2199 BS).
        /// </exception>
        public static NepaliDate ToNepaliDate(this DateTime englishDate) => new NepaliDate(englishDate);
    }
}
