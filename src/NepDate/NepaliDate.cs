using NepDate.Core.Dictionaries;
using NepDate.Extensions;
using System;
using static NepDate.Exceptions.NepDateException;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        #region Ctor
        /// <summary>
        /// Used internally for constructor validation
        /// </summary>
        /// <exception cref="InvalidNepaliDateFormatException"></exception>
        private void ValidateAndThrow()
        {
            if (Day < 1 || Day > MonthEndDay || Month < 1 || Month > 12 || Year < _minYear || Year > _maxYear)
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


        public NepaliDate AddMonths(double months, bool awayFromMonthEnd = false)
        {
            if (months < 0)
            {
                return SubtractMonths(Math.Abs(months), awayFromMonthEnd);
            }

            int roundedMonths = (int)Math.Round(months, 0, MidpointRounding.AwayFromZero);

            if (months != roundedMonths)
            {
                return AddDays(Math.Round(months * 30.41666666666667, 0, MidpointRounding.AwayFromZero));
            }

            bool returnFirstDay = false;//added for future

            int nextYear = Year;
            int nextMonth = Month + roundedMonths;
            int nextMonthDay = 1;


            while (nextMonth > 12)
            {
                nextYear++;
                nextMonth -= 12;
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


        private NepaliDate SubtractMonths(double months, bool awayFromMonthEnd = false)
        {
            if (months < 0)
            {
                return AddMonths(Math.Abs(months), awayFromMonthEnd);
            }

            int roundedMonths = (int)Math.Round(months, 0, MidpointRounding.AwayFromZero);

            if (months != roundedMonths)
            {
                return AddDays(-Math.Round(months * 30.41666666666667, 0, MidpointRounding.AwayFromZero));
            }

            bool returnFirstDay = false;//added for future

            int previousYear = Year;
            int previousMonth = Month - roundedMonths;
            int previousMonthDay = 1;

            //if (previousMonth < 1)
            //{
            //    previousYear--;
            //    previousMonth = 12;
            //}

            while (previousMonth < 1)
            {
                previousYear--;
                previousMonth = previousMonth + 12;
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

        public bool IsToday()
        {
            return DateTime.Now.ToNepaliDate() == this;
        }

        public bool IsYesterday()
        {
            return DateTime.Now.AddDays(-1).ToNepaliDate() == this;
        }
        public bool IsTomorrow()
        {
            return DateTime.Now.AddDays(1).ToNepaliDate() == this;
        }

        //public static (int year, int month, int day) SplitNepaliDate(string rawNepaliDate)
        //{
        //    if (string.IsNullOrEmpty(rawNepaliDate))
        //    {
        //        throw new InvalidNepaliDateArgumentException();
        //    }

        //    var span = rawNepaliDate.Split(new char[] { '-', '/', ' ', '_', '\\' }, StringSplitOptions.RemoveEmptyEntries).AsSpan();

        //    if (span.Length != 3)
        //    {
        //        throw new InvalidNepaliDateFormatException();
        //    }

        //    return (int.Parse(span[0]), int.Parse(span[1]), int.Parse(span[2]));
        //}

        private static (int year, int month, int day) SplitNepaliDate(string rawNepaliDate)
        {
            if (string.IsNullOrEmpty(rawNepaliDate))
            {
                throw new InvalidNepaliDateArgumentException();
            }

            //string trimmedDate = rawNepaliDate.Trim().Replace("-", "/").Replace(".", "/").Replace("_", "/").Replace("\\", "/").Replace(" ", "/");
            //string[] splitDate = trimmedDate.Split('/');

            int[] splitDate = Array.ConvertAll(rawNepaliDate.Trim().Split(new char[] { '-', '/', ' ', '_', '\\' }, StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));

            if (splitDate.Length != 3)
            {
                throw new InvalidNepaliDateFormatException();
            }

            return (splitDate[0], splitDate[1], splitDate[2]);
        }
    }
}
