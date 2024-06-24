using System;
using System.Collections.Generic;

namespace NepDate.Core.Dictionaries
{
    internal static class Unicode
    {
        internal static readonly Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {nameof(NepaliMonths.Baishakh), "बैशाख" },
            {nameof(NepaliMonths.Jestha), "जेठ" },
            {nameof(NepaliMonths.Ashad), "असार" },
            {nameof(NepaliMonths.Shrawan), "साउन" },
            {nameof(NepaliMonths.Bhadra), "भदौ" },
            {nameof(NepaliMonths.Ashoj), "असोज" },
            {nameof(NepaliMonths.Kartik), "कार्तिक" },
            {nameof(NepaliMonths.Mangsir), "मंसिर" },
            {nameof(NepaliMonths.Poush), "पुष" },
            {nameof(NepaliMonths.Magh), "माघ" },
            {nameof(NepaliMonths.Falgun), "फागुन" },
            {nameof(NepaliMonths.Chaitra), "चैत" },

            {nameof(DayOfWeek.Sunday), "आइतवार" },
            {nameof(DayOfWeek.Monday), "सोमवार" },
            {nameof(DayOfWeek.Tuesday), "मङ्गलवार" },
            {nameof(DayOfWeek.Wednesday),"बुधवार" },
            {nameof(DayOfWeek.Thursday), "बिहिवार" },
            {nameof(DayOfWeek.Friday), "शुक्रवार" },
            {nameof(DayOfWeek.Saturday), "शनिवार" },
        };
    }
}
