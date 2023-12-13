using System;
using System.Collections.Generic;
using System.Linq;
using NepDate.Core.Constants;
using NepDate.Core.Dictionaries;
using NepDate.Core.Enums;
using NepDate.Extensions;
using static NepDate.Exceptions.NepDateException;

namespace NepDate
{
    public readonly partial struct NepaliDate : IComparable<NepaliDate>, IEquatable<NepaliDate>
    {
        #region Ctor
        /// <summary>
        /// Used internally for constructor validation
        /// </summary>
        /// <exception cref="InvalidNepaliDateFormatException"></exception>
        private void ValidateAndThrow()
        {
            if (Day < 1 || Day > MonthEndDay || Month < 1 || Month > 12 || Year < NepDateConstants._minYear || Year > NepDateConstants._maxYear)
            {
                throw new InvalidNepaliDateFormatException();
            }
        }




        /// <summary>
        /// Initializes a new instance of the <see cref="NepaliDate"/> class with the specified Nepali date components.
        /// </summary>
        /// <param name="yearBs">The year component of the Nepali date.</param>
        /// <param name="monthBs">The month component of the Nepali date.</param>
        /// <param name="dayBs">The day component of the Nepali date.</param>
        public NepaliDate(int yearBs, int monthBs, int dayBs)
        {
            (Year, Month, Day) = (yearBs, monthBs, dayBs);
            EnglishDate = DictionaryBridge.NepToEng.GetEnglishDate(Year, Month, Day);

            ValidateAndThrow();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NepaliDate"/> class with the specified raw Nepali date string.
        /// </summary>
        /// <param name="rawNepaliDate">The raw Nepali date string in the format "YYYY/MM/DD".</param>
        public NepaliDate(string rawNepaliDate)
        {
            (Year, Month, Day) = SplitNepaliDate(rawNepaliDate);

            EnglishDate = DictionaryBridge.NepToEng.GetEnglishDate(Year, Month, Day);

            ValidateAndThrow();
        }

        public NepaliDate(string rawNepaliDate, bool autoAdjust, bool monthInMiddle = true)
        {
            const int currentMillennium = 2;

            (Year, Month, Day) = SplitNepaliDate(rawNepaliDate);

            if (autoAdjust)
            {
                if (Day.ToString().Length >= 3 || Day > 32)
                {
                    (Year, Day) = (Day, Year);
                }

                if (!monthInMiddle)
                {
                    (Month, Day) = (Day, Month);
                }

                if (Month > 12 && Day < 12)
                {
                    (Month, Day) = (Day, Month);
                }

                if (Year.ToString().Length <= 3)
                {
                    Year = int.Parse(string.Concat(currentMillennium.ToString(), Year.ToString("D3")));
                }
            }

            EnglishDate = DictionaryBridge.NepToEng.GetEnglishDate(Year, Month, Day);

            ValidateAndThrow();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NepaliDate"/> class with the specified English DateTime.
        /// </summary>
        /// <param name="englishDate">The English DateTime to convert to Nepali date.</param>
        public NepaliDate(DateTime englishDate)
        {
            (Year, Month, Day) = DictionaryBridge.EngToNep.GetNepaliDate(englishDate.Year, englishDate.Month, englishDate.Day);
            EnglishDate = englishDate;

            ValidateAndThrow();
        }
        #endregion


        /// <summary>
        /// Adds months based on the current Nepali date object.
        /// </summary>
        /// <param name="returnFirstDay">A boolean indicating whether to return the first day of the next month.</param>
        /// <returns>A NepaliDate object representing the Nepali date for the next month.</returns>
        public NepaliDate AddMonths(double months, bool awayFromMonthEnd = false)
        {
            int roundedMonths = (int)Math.Round(months, 0, MidpointRounding.AwayFromZero);

            if (months != roundedMonths)
            {
                return AddDays(Math.Round(months * 30.41666666666667, 0, MidpointRounding.AwayFromZero));
            }

            bool returnFirstDay = false;//added for future

            int nextYear = Year;
            int nextMonth = Month + roundedMonths;
            int nextMonthDay = 1;

            if (nextMonth > 12)
            {
                nextYear++;
                nextMonth = 1;
            }

            var nextMonthNepDate = new NepaliDate(nextYear, nextMonth, nextMonthDay);

            if (returnFirstDay)
            {
                return nextMonthNepDate;
            }

            if (awayFromMonthEnd)
            {
                if (Day > nextMonthNepDate.MonthEndDay)
                {
                    nextMonthDay = Day - nextMonthNepDate.MonthEndDay;
                    nextMonth++;

                    if (nextMonth > 12)
                    {
                        nextYear++;
                        nextMonth = 1;
                    }
                }
                else
                {
                    nextMonthDay = Day;
                }
            }
            else
            {
                nextMonthDay = Math.Min(nextMonthNepDate.MonthEndDay, Day);
            }

            return new NepaliDate(nextYear, nextMonth, nextMonthDay);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="months"></param>
        /// <param name="awayFromMonthEnd"></param>
        /// <returns></returns>
        public NepaliDate SubtractMonths(double months, bool awayFromMonthEnd = false)
        {
            int roundedMonths = (int)Math.Round(months, 0, MidpointRounding.AwayFromZero);

            if (months != roundedMonths)
            {
                return AddDays(-Math.Round(months * 30.41666666666667, 0, MidpointRounding.AwayFromZero));
            }

            bool returnFirstDay = false;//added for future

            int previousYear = Year;
            int previousMonth = Month - roundedMonths;
            int previousMonthDay = 1;

            if (previousMonth < 1)
            {
                previousYear--;
                previousMonth = 12;
            }

            var previousMonthNepDate = new NepaliDate(previousYear, previousMonth, previousMonthDay);

            if (returnFirstDay)
            {
                return previousMonthNepDate;
            }

            if (awayFromMonthEnd)
            {
                if (Day > previousMonthNepDate.MonthEndDay)
                {
                    previousMonthDay = Day - previousMonthNepDate.MonthEndDay;
                    previousMonth++;

                    if (previousMonth > 12)
                    {
                        previousYear++;
                        previousMonth = 1;
                    }
                }
                else
                {
                    previousMonthDay = Day;
                }
            }
            else
            {
                previousMonthDay = Math.Min(previousMonthNepDate.MonthEndDay, Day);
            }

            return new NepaliDate(previousYear, previousMonth, previousMonthDay);
        }


        /// <summary>
        /// Returns a new Nepali date that adds the specified number of days to this instance.
        /// </summary>
        /// <param name="days">The number of days to add.</param>
        public NepaliDate AddDays(double days)
        {
            return EnglishDate.AddDays(days).ToNepaliDate();
        }


        /// <summary>
        /// Returns a new <see cref="TimeSpan"/> object representing the time interval between the two specified Nepali dates.
        /// </summary>
        /// <param name="nepDateTo">The Nepali date to subtract from this instance.</param>
        public TimeSpan Subtract(NepaliDate nepDateTo)
        {
            return this - nepDateTo;
        }


        /// <summary>
        /// Determines whether the current Nepali year is a leap year.
        /// </summary>
        public bool IsLeapYear()
        {
            return Year % 4 == 0 && (Year % 100 != 0 || Year % 400 == 0);
        }


        private static (int year, int month, int day) SplitNepaliDate(string rawNepaliDate)
        {
            const byte splitLength = 3;
            if (string.IsNullOrEmpty(rawNepaliDate))
            {
                throw new InvalidNepaliDateArgumentException();
            }

            string trimmedDate = rawNepaliDate.Trim().Replace("-", "/").Replace(".", "/").Replace("_", "/");
            string[] splitDate = trimmedDate.Split('/');

            if (splitDate.Length != splitLength)
            {
                throw new InvalidNepaliDateFormatException();
            }
            return (int.Parse(splitDate[0]), int.Parse(splitDate[1]), int.Parse(splitDate[2]));
        }
    }
}
