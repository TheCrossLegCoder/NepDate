using NepDate.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NepDate.Core.Dictionaries
{
    internal static class Unicode
    {
        internal static readonly Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {nameof(NepaliMonths.Baishakh), "बैशाख" },
            {nameof(NepaliMonths.Jestha), "जेठ" },
            {nameof(NepaliMonths.Ashad), "असाद" },
            {nameof(NepaliMonths.Shrawan), "श्रावण" },
            {nameof(NepaliMonths.Bhadra), "भाद्र" },
            {nameof(NepaliMonths.Ashoj), "अशोक" },
            {nameof(NepaliMonths.Kartik), "कार्तिक" },
            {nameof(NepaliMonths.Mangsir), "मङ्गसिर" },
            {nameof(NepaliMonths.Poush), "पुस" },
            {nameof(NepaliMonths.Magh), "माघ" },
            {nameof(NepaliMonths.Falgun), "फाल्गुन" },
            {nameof(NepaliMonths.Chaitra), "चैत्र" },

            {nameof(DayOfWeek.Sunday), "आइतबार" },
            {nameof(DayOfWeek.Monday), "सोमबार" },
            {nameof(DayOfWeek.Tuesday), "मंगलबार" },
            {nameof(DayOfWeek.Wednesday),"बुधबार" },
            {nameof(DayOfWeek.Thursday), "बिहीबार" },
            {nameof(DayOfWeek.Friday), "शुक्रबार" },
            {nameof(DayOfWeek.Saturday), "शनिबार" },
        };
    }
}
