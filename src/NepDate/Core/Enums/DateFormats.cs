namespace NepDate.Core.Enums
{
    public enum DateFormats
    {
        /// <summary>
        /// Probable output example : 2081/01/15
        /// </summary>
        YearMonthDay = 0,

        /// <summary>
        /// Probable output example : 2081/15/01
        /// </summary>
        YearDayMonth = 1,

        /// <summary>
        /// Probable output example : 01/2081/15
        /// </summary>
        MonthYearDay = 2,

        /// <summary>
        /// Probable output example : 01/15/2081
        /// </summary>
        MonthDayYear = 3,

        /// <summary>
        /// Probable output example : 15/2081/01
        /// </summary>
        DayYearMonth = 4,

        /// <summary>
        /// Probable output example : 15/01/2081
        /// </summary>
        DayMonthYear = 5,
    }
}
