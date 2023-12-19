using NepDate.Core.Enums;
using NepDate.Exceptions;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        /// <summary>
        /// Gets the fiscal year quarter for the current Nepali date.
        /// </summary>
        private FiscalYearQuarters FiscalYearQuarter()
        {
            switch (Month)
            {
                case 4:
                case 5:
                case 6:
                    return FiscalYearQuarters.First;
                case 7:
                case 8:
                case 9:
                    return FiscalYearQuarters.Second;
                case 10:
                case 11:
                case 12:
                    return FiscalYearQuarters.Third;
                case 1:
                case 2:
                case 3:
                    return FiscalYearQuarters.Fourth;
                default:
                    throw new NepDateException.InvalidNepaliDateArgumentException();
            }
        }


        /// <summary>
        /// Calculates the start date of the fiscal year for the specified year offset.
        /// </summary>
        /// <param name="yearOffset">The offset to adjust the year.</param>
        /// <returns>The start date of the fiscal year.</returns>
        public NepaliDate FiscalYearStartDate(int yearOffset = 0)
        {
            if (Month <= 3)
            {
                yearOffset--;
            }
            return new NepaliDate(Year + yearOffset, 4, 1);
        }


        /// <summary>
        /// Calculates the end date of the fiscal year for the specified year offset.
        /// </summary>
        /// <param name="yearOffset">The offset to adjust the year.</param>
        /// <returns>The end date of the fiscal year.</returns>
        public NepaliDate FiscalYearEndDate(int yearOffset = 0)
        {
            if (Month >= 4)
            {
                yearOffset++;
            }

            return new NepaliDate(Year + yearOffset, 3, 1).MonthEndDate;
        }


        /// <summary>
        /// Gets the start and end date of the fiscal year for the specified year offset.
        /// </summary>
        /// <param name="yearOffset">The offset to adjust the year.</param>
        /// <returns>A tuple containing the start and end date of the fiscal year.</returns>
        public (NepaliDate startDate, NepaliDate endDate) FiscalYearStartAndEndDate(int yearOffset = 0)
        {
            return (FiscalYearStartDate(yearOffset), FiscalYearEndDate(yearOffset));
        }


        /// <summary>
        /// Calculates the start date of a fiscal year quarter in NepaliDate format.
        /// </summary>
        /// <param name="fiscalYearQuarters">The fiscal year quarter (Current or specific).</param>
        /// <param name="yearOffset">An optional year offset for calculations.</param>
        /// <returns>The NepaliDate representing the start date of the fiscal year quarter.</returns>
        public NepaliDate FiscalYearQuarterStartDate(FiscalYearQuarters fiscalYearQuarters = FiscalYearQuarters.Current, int yearOffset = 0)
        {
            if (fiscalYearQuarters == FiscalYearQuarters.Current)
            {
                fiscalYearQuarters = FiscalYearQuarter();
            }

            if (fiscalYearQuarters == FiscalYearQuarters.Fourth)
            {
                yearOffset++;
            }

            return new NepaliDate(Year + yearOffset, (int)fiscalYearQuarters, 1);
        }


        /// <summary>
        /// Calculates the end date of a fiscal year quarter in NepaliDate format.
        /// </summary>
        /// <param name="fiscalYearQuarters">The fiscal year quarter (Current or specific).</param>
        /// <param name="yearOffset">An optional year offset for calculations.</param>
        /// <returns>The NepaliDate representing the end date of the fiscal year quarter.</returns>
        public NepaliDate FiscalYearQuarterEndDate(FiscalYearQuarters fiscalYearQuarters = FiscalYearQuarters.Current, int yearOffset = 0)
        {
            if (fiscalYearQuarters == FiscalYearQuarters.Current)
            {
                fiscalYearQuarters = FiscalYearQuarter();
            }

            if (fiscalYearQuarters == FiscalYearQuarters.Fourth)
            {
                yearOffset++;
            }
            return new NepaliDate(Year + yearOffset, (int)fiscalYearQuarters + 2, 1).MonthEndDate;
        }


        /// <summary>
        /// Calculates both the start and end dates of a fiscal year quarter in NepaliDate format.
        /// </summary>
        /// <param name="fiscalYearQuarters">The fiscal year quarter (Current or specific).</param>
        /// <param name="yearOffset">An optional year offset for calculations.</param>
        /// <returns>A tuple containing the start and end dates of the fiscal year quarter.</returns>
        public (NepaliDate startDate, NepaliDate endDate) FiscalYearQuarterStartAndEndDate(FiscalYearQuarters fiscalYearQuarters = FiscalYearQuarters.Current, int yearOffset = 0)
        {
            return (FiscalYearQuarterStartDate(fiscalYearQuarters, yearOffset), FiscalYearQuarterEndDate(fiscalYearQuarters, yearOffset));
        }



        /// <summary>
        /// Gets the start and end date of the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year. (ie. 2080)</param>
        /// <returns>A tuple containing the start and end date of the fiscal year.</returns>
        public static (NepaliDate startDate, NepaliDate endDate) GetFiscalYearStartAndEndDate(int fiscalYear)
        {
            return (GetFiscalYearStartDate(fiscalYear), GetFiscalYearEndDate(fiscalYear));
        }


        /// <summary>
        /// Gets the start date of the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year. (ie. 2080)</param>
        /// <returns>The start date of the fiscal year.</returns>
        public static NepaliDate GetFiscalYearStartDate(int fiscalYear)
        {
            return new NepaliDate(fiscalYear, 4, 1);
        }


        /// <summary>
        /// Gets the end date of the specified fiscal year.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year. (ie. 2080)</param>
        /// <returns>The end date of the fiscal year.</returns>
        public static NepaliDate GetFiscalYearEndDate(int fiscalYear)
        {
            return new NepaliDate(fiscalYear + 1, 3, 1).MonthEndDate;
        }


        /// <summary>
        /// Gets the start date of a fiscal year quarter based on the provided fiscal year and month.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year for the calculation.</param>
        /// <param name="month">The month within the fiscal year.</param>
        /// <returns>The NepaliDate representing the start date of the fiscal year quarter.</returns>
        public static NepaliDate GetFiscalYearQuarterStartDate(int fiscalYear, int month)
        {
            return new NepaliDate(fiscalYear, month, 1).FiscalYearQuarterStartDate();
        }


        /// <summary>
        /// Gets the end date of a fiscal year quarter based on the provided fiscal year and month.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year for the calculation.</param>
        /// <param name="month">The month within the fiscal year.</param>
        /// <returns>The NepaliDate representing the end date of the fiscal year quarter.</returns>
        public static NepaliDate GetFiscalYearQuarterEndDate(int fiscalYear, int month)
        {
            return new NepaliDate(fiscalYear, month, 1).FiscalYearQuarterEndDate();
        }


        /// <summary>
        /// Gets both the start and end dates of a fiscal year quarter based on the provided fiscal year and month.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year for the calculation.</param>
        /// <param name="month">The month within the fiscal year.</param>
        /// <returns>A tuple containing the start and end dates of the fiscal year quarter.</returns>
        public static (NepaliDate startDate, NepaliDate endDate) GetFiscalYearQuarterStartAndEndDate(int fiscalYear, int month)
        {
            return (GetFiscalYearQuarterStartDate(fiscalYear, month), GetFiscalYearQuarterEndDate(fiscalYear, month));
        }
    }
}
