using NepDate.Core.Dictionaries;
using NepDate.Extensions;
using System;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        private const ushort _minYear = 1987;
        private const ushort _maxYear = 2099;
        internal int AsInteger => int.Parse(string.Concat($"{Year:D4}", $"{Month:D2}", $"{Day:D2}"));


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
        /// Gets the day of the week represented by this instance.
        /// </summary>
        public DayOfWeek DayOfWeek => EnglishDate.DayOfWeek;


        /// <summary>
        /// Gets the day of the year represented by this instance.
        /// </summary>
        public int DayOfYear => EnglishDate.DayOfYear;

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


        /// <summary>
        /// Gets the current/today Nepali date.
        /// </summary>
        public static NepaliDate Now => DateTime.Now.ToNepaliDate();


        /// <summary>
        /// Represents the smallest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MinValue = new NepaliDate(_minYear, 1, 1);

        /// <summary>
        /// Represents the largest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MaxValue = new NepaliDate(_maxYear, 12, 1).MonthEndDate;
    }
}
