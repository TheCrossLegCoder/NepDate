using System;

namespace NepDate.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Convert DateTime to NepaliDate
        /// </summary>
        /// <param name="englishDate">English Date</param>
        /// <returns>Nepali Date</returns>
        public static NepaliDate ToNepaliDate(this DateTime englishDate) => new NepaliDate(englishDate);
    }
}
