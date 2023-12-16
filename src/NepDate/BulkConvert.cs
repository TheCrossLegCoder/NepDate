using System;
using System.Collections.Generic;
using System.Linq;

namespace NepDate
{
    public partial struct NepaliDate
    {
        public class BulkConvert
        {
            public static List<NepaliDate> ToNepaliDate(List<DateTime> engDates)
            {
                return engDates.Select(item => new NepaliDate(item)).ToList();
            }

            public static List<DateTime> ToEnglishDate(List<string> nepDates)
            {
                return nepDates.Select(item => NepaliDate.Parse(item).EnglishDate).ToList();
            }

            public static List<DateTime> ToEnglishDate(List<NepaliDate> nepDates)
            {
                return nepDates.Select(item => item.EnglishDate).ToList();
            }
        }
    }
}
