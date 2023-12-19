using System;
using System.Collections.Generic;
using System.Linq;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        /// <summary>
        /// Provides methods for bulk conversion between English and Nepali dates.
        /// </summary>
        public class BulkConvert
        {
            /// <summary>
            /// Converts a collection of English dates to Nepali dates.
            /// </summary>
            /// <param name="engDates">The collection of English dates to convert.</param>
            /// <returns>An IEnumerable of NepaliDate corresponding to the input English dates.</returns>
            public static IEnumerable<NepaliDate> ToNepaliDate(IEnumerable<DateTime> engDates)
            {
                return engDates.Select(item => new NepaliDate(item));
            }


            /// <summary>
            /// Converts a collection of Nepali dates represented as strings to English dates.
            /// </summary>
            /// <param name="nepDates">The collection of Nepali dates (as strings) to convert.</param>
            /// <returns>An IEnumerable of DateTime representing the English dates corresponding to the input Nepali dates.</returns>
            public static IEnumerable<DateTime> ToEnglishDate(IEnumerable<string> nepDates)
            {
                return nepDates.Select(item => Parse(item).EnglishDate);
            }


            /// <summary>
            /// Converts a collection of Nepali dates to English dates.
            /// </summary>
            /// <param name="nepDates">The collection of Nepali dates to convert.</param>
            /// <returns>An IEnumerable of DateTime representing the English dates corresponding to the input Nepali dates.</returns>
            public static IEnumerable<DateTime> ToEnglishDate(IEnumerable<NepaliDate> nepDates)
            {
                return nepDates.Select(item => item.EnglishDate);
            }
        }
    }
}
