﻿using System;
using NepDate.Core;
using NepDate.Core.Enums;
using static NepDate.Exceptions;

namespace NepDate
{
    public readonly struct NepaliDate : IComparable<NepaliDate>, IEquatable<NepaliDate>
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
        /// Used internally for constructor validation
        /// </summary>
        /// <exception cref="InvalidNepaliDateFormatException"></exception>
        private void ValidateAndThrow()
        {
            if (Day < 1 || Day > MonthEndDay || Month < 1 || Month > 12 || Year < Constants._minYear || Year > Constants._maxYear)
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
            EnglishDate = Handlers.NepToEng.GetEnglishDate(Year, Month, Day);

            ValidateAndThrow();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NepaliDate"/> class with the specified raw Nepali date string.
        /// </summary>
        /// <param name="rawNepaliDate">The raw Nepali date string in the format "YYYY/MM/DD".</param>
        public NepaliDate(string rawNepaliDate)
        {
            (Year, Month, Day) = SplitNepaliDate(rawNepaliDate);

            EnglishDate = Handlers.NepToEng.GetEnglishDate(Year, Month, Day);

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

            EnglishDate = Handlers.NepToEng.GetEnglishDate(Year, Month, Day);

            ValidateAndThrow();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NepaliDate"/> class with the specified English DateTime.
        /// </summary>
        /// <param name="englishDate">The English DateTime to convert to Nepali date.</param>
        public NepaliDate(DateTime englishDate)
        {
            (Year, Month, Day) = Handlers.EngToNep.GetNepaliDate(englishDate.Year, englishDate.Month, englishDate.Day);
            EnglishDate = englishDate;

            ValidateAndThrow();
        }

        /// <summary>
        /// Gets the day of the week represented by this Nepali date.
        /// </summary>
        public DayOfWeek DayOfWeek => EnglishDate.DayOfWeek;

        /// <summary>
        /// Gets the last day of the month represented by this Nepali date.
        /// </summary>
        public int MonthEndDay
            => Handlers.NepToEng.GetNepaliMonthEndDay(Year, Month);

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
        /// Calculates the Nepali date for the next month based on the current Nepali date object.
        /// </summary>
        /// <param name="returnFirstDay">A boolean indicating whether to return the first day of the next month.</param>
        /// <returns>A NepaliDate object representing the Nepali date for the next month.</returns>
        public NepaliDate NextMonth(bool returnFirstDay = true)
        {
            int nextYear = Year;
            int nextMonth = Month + 1;
            int nextMonthDay = 1;

            if (Month == 12)
            {
                nextYear++;
                nextMonth = 1;
            }

            NepaliDate nextMonthNepDate = new NepaliDate(nextYear, nextMonth, nextMonthDay);

            if (!returnFirstDay)
            {
                nextMonthDay = nextMonthNepDate.MonthEndDay < Day ? nextMonthNepDate.MonthEndDay : Day;
                return new NepaliDate(nextYear, nextMonth, nextMonthDay);
            }

            return nextMonthNepDate;
        }

        // <summary>
        /// Calculates the Nepali date for the previous month based on the current Nepali date object.
        /// </summary>
        /// <param name="returnFirstDay">A boolean indicating whether to return the first day of the previous month.</param>
        /// <returns>A NepaliDate object representing the Nepali date for the previous month.</returns>
        public NepaliDate PreviousMonth(bool returnFirstDay = true)
        {
            int prevYear = Year;
            int prevMonth = Month - 1;
            int prevMonthDay = 1;

            if (Month == 1)
            {
                prevYear--;
                prevMonth = 12;
            }

            NepaliDate prevMonthNepDate = new NepaliDate(prevYear, prevMonth, prevMonthDay);

            if (!returnFirstDay)
            {
                prevMonthDay = prevMonthNepDate.MonthEndDay < Day ? prevMonthNepDate.MonthEndDay : Day;
                return new NepaliDate(prevYear, prevMonth, prevMonthDay);
            }

            return prevMonthNepDate;
        }

        public NepaliDate FinancialYearStartDate(int yearAdjustment = 0)
        {
            if (Month <= 3)
            {
                yearAdjustment--;
            }
            return new NepaliDate(Year + yearAdjustment, 4, 1);
        }

        public NepaliDate FinancialYearEndDate(int yearAdjustment = 0)
        {
            if(Month >= 4)
            {
                yearAdjustment++;
            }
            var endDate = new NepaliDate(Year + yearAdjustment, 3, 1);
            endDate = endDate.MonthEndDate;
            return endDate;
        }

        /// <summary>
        /// Gets the integer representation of this Nepali date in the format "YYYYMMDD".
        /// </summary>
        private int AsInteger
            => int.Parse(string.Concat($"{Year:D4}", $"{Month:D2}", $"{Day:D2}"));

        /// <summary>
        /// Gets the current/today Nepali date.
        /// </summary>
        public static NepaliDate Now => DateTime.Now.ToNepaliDate();

        /// <summary>
        /// Min year value
        /// </summary>
        public static readonly int MinYear = Constants._minYear;

        /// <summary>
        /// Max year value
        /// </summary>
        public static readonly int MaxYear = Constants._maxYear;

        /// <summary>
        /// Represents the smallest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MinValue = Parse($"{Constants._minYear}/01/01");

        /// <summary>
        /// Represents the largest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MaxValue = Parse($"{Constants._maxYear}/12/30");

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

        /// <summary>
        /// Returns a new Nepali date that adds the specified number of days to this instance.
        /// </summary>
        /// <param name="days">The number of days to add.</param>
        public NepaliDate AddDays(int days)
        {
            return EnglishDate.AddDays(days).ToNepaliDate();
        }

        /// <summary>
        /// Tries to parse the specified string representation of a Nepali date and returns a value indicating whether the parsing succeeded.
        /// </summary>
        /// <param name="rawNepDate">The raw Nepali date string in the format "YYYY/MM/DD".</param>
        /// <param name="result">When this method returns, contains the NepaliDate value equivalent to the Nepali date contained in rawNepDate, if the parsing succeeded, or default if the parsing failed.</param>
        /// <returns>true if the parsing succeeded; otherwise, false.</returns>
        public static bool TryParse(string rawNepDate, out NepaliDate result)
        {
            try
            {
                result = Parse(rawNepDate);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        public static bool TryParse(string rawNepDate, out NepaliDate result, bool autoAdjust, bool monthInMiddle = true)
        {
            try
            {
                result = Parse(rawNepDate, autoAdjust, monthInMiddle);
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Parses the specified string representation of a Nepali date and returns a NepaliDate object.
        /// </summary>
        /// <param name="rawNepDate">The raw Nepali date string in the format "YYYY/MM/DD".</param>
        /// <returns>A NepaliDate object that is equivalent to the Nepali date contained in rawNepDate.</returns>
        public static NepaliDate Parse(string rawNepaliDate)
        {
            return new NepaliDate(rawNepaliDate);
        }

        public static NepaliDate Parse(string rawNepaliDate, bool autoAdjust, bool monthInMiddle = true)
        {
            return new NepaliDate(rawNepaliDate, autoAdjust, monthInMiddle);
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
