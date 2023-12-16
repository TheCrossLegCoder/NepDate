using NepDate.Core.Dictionaries;
using NepDate.Core.Enums;
using System;

namespace NepDate
{
    public partial struct NepaliDate
    {
        /// <summary>
        /// Year as integer
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// Month as integer
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// Day as integer
        /// </summary>
        public int Day { get; }
        public DateTime EnglishDate { get; }




        /// <summary>
        /// Gets the day of the week represented by this Nepali date.
        /// </summary>
        public DayOfWeek DayOfWeek => EnglishDate.DayOfWeek;


        /// <summary>
        /// Gets the last day of the month represented by this Nepali date.
        /// </summary>
        public int MonthEndDay
            => DictionaryBridge.NepToEng.GetNepaliMonthEndDay(Year, Month);


        /// <summary>
        /// Gets the Nepali date representing the last day of the month.
        /// </summary>
        public NepaliDate MonthEndDate
            => new NepaliDate(Year, Month, MonthEndDay);


        /// <summary>
        /// Gets the Nepali month full name based on the current Nepali date object.
        /// </summary>
        public NepaliMonths MonthName => (NepaliMonths)Month;
    }
}
