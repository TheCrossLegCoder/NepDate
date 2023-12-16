namespace NepDate
{
    public partial struct NepaliDate
    {
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
            if (Month >= 4)
            {
                yearAdjustment++;
            }
            var endDate = new NepaliDate(Year + yearAdjustment, 3, 1);
            endDate = endDate.MonthEndDate;
            return endDate;
        }

        public class FinancialYear
        {
            public static (NepaliDate startDate, NepaliDate endDate) GetFinancialYearStartAndEndDate(int financialYear)
            {
                return (GetFinancialYearStartDate(financialYear), GetFinancialYearEndDate(financialYear));
            }

            public static NepaliDate GetFinancialYearStartDate(int financialYear)
            {
                return new NepaliDate(financialYear, 4, 1);
            }

            public static NepaliDate GetFinancialYearEndDate(int financialYear)
            {
                return new NepaliDate(financialYear + 1, 3, 1).MonthEndDate;
            }
        }
    }
}
