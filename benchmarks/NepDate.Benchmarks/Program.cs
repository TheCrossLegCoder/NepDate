using NepDate;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        //BenchmarkRunner.Run<Benchmarks>(ManualConfig.Create(DefaultConfig.Instance).WithOptions(ConfigOptions.DisableOptimizationsValidator));

        //var today = NepDate.NepaliDate.Now;
        //var remainingDaysOnThisMonth = today.MonthEndDay - today.Day;
        //Console.WriteLine(remainingDaysOnThisMonth.ToString());

        //var nepDateAddMonthTest1 = NepDate.NepaliDate.Parse("2080/01/01").ToLongDateUnicodeString(true, true, true);
        //var nepDateAddMonthTest3 = NepDate.NepaliDate.Parse("2080/01/01").ToString("yyyy/mm/dd", CultureInfo.InvariantCulture);



        //Console.WriteLine(nepDateAddMonthTest1);
        //Console.WriteLine(nepDateAddMonthTest3);

        //Console.WriteLine(new NepaliDate(2079, 12, 12).EnglishDate);
        //Console.WriteLine(new NepaliDate(2079, 12, 12).ToString());

        //var currentDate = new DateTime(2024, 06, 12);
        //for (int i = 0; i < 1000; i++)
        //{
        //    var nepDateStr = currentDate.ToNepaliDate().ToString();
        //    var otherDate = NepaliDateConverter.DateConverter.ConvertToNepali(currentDate.Year, currentDate.Month, currentDate.Day);
        //    var otherDateStr = $"{otherDate.Year:D4}/{otherDate.Month:D2}/{otherDate.Day:D2}";
        //    if (nepDateStr != otherDateStr)
        //    {
        //        Console.WriteLine(nepDateStr);
        //    }
        //    currentDate = currentDate.AddDays(1);
        //}


        var nepDate = new NepaliDate("2081/04/15");

        nepDate.FiscalYearStartDate(); // 2081/04/01
        nepDate.FiscalYearEndDate(); // 2082/03/31
        nepDate.FiscalYearStartAndEndDate(); // (2081/04/01, 2082/03/31)
        nepDate.FiscalYearQuarterStartDate(); // 2081/04/01
        nepDate.FiscalYearQuarterEndDate(); // 2081/06/30
        nepDate.FiscalYearQuarterStartAndEndDate(); // (2081/04/01, 2081/06/30)

        NepaliDate.GetFiscalYearStartDate(2080); // 2080/04/01
        NepaliDate.GetFiscalYearEndDate(2080); // 2081/03/31
        NepaliDate.GetFiscalYearStartAndEndDate(2080); // (2080/04/01, 2081/03/31)
        NepaliDate.GetFiscalYearQuarterStartDate(2080, 1); // 2081/01/01
        NepaliDate.GetFiscalYearQuarterEndDate(2080, 1); // 2081/03/31
        NepaliDate.GetFiscalYearQuarterStartAndEndDate(2080, 1); // (2081/01/01, 2081/03/31)
    }
}
