using System;
using static NepDate.Exceptions.NepDateException;

namespace NepDate.Core.Dictionaries
{
    internal static class DictionaryBridge
    {
        internal static class NepToEng
        {
            internal static DateTime GetEnglishDate(int nepYear, int nepMonth, int nepDay)
            {
                if (NepaliToEnglish.data.TryGetValue((nepYear, nepMonth), out (int NepMonthEndDay, int EngYear, int EngMonth, int EngDay) dictVal))
                {
                    return new DateTime(dictVal.EngYear, dictVal.EngMonth, dictVal.EngDay).AddDays(nepDay - dictVal.NepMonthEndDay);
                }

                throw new InvalidNepaliDateArgumentException();
            }

            internal static int GetNepaliMonthEndDay(int nepYear, int nepMonth)
            {
                if (NepaliToEnglish.data.TryGetValue((nepYear, nepMonth), out var dictVal))
                {
                    return dictVal.NepMonthEndDay;
                }
                throw new InvalidNepaliDateArgumentException();
            }
        }

        internal static class EngToNep
        {
            internal static (int, int, int) GetNepaliDate(int engYear, int engMonth, int engDay)
            {
                _ = EnglishToNepali.data.TryGetValue((engYear, engMonth), out (int EngMonthEndDay, int NepYear, int NepMonth, int NepDay) dictVal);
                return SubtractNepaliDays(dictVal.NepYear, dictVal.NepMonth, dictVal.NepDay, (dictVal.EngMonthEndDay - engDay));
            }

            private static (int yearBs, int monthBs, int dayBs) SubtractNepaliDays(int yearBs, int monthBs, int dayBs, int daysToSubtract)
            {
                int newDayBs = dayBs - daysToSubtract;

                while (newDayBs <= 0)
                {
                    if (monthBs == 1)
                    {
                        yearBs--;
                        monthBs = 12;
                    }
                    else
                    {
                        monthBs--;
                    }
                    newDayBs += NepToEng.GetNepaliMonthEndDay(yearBs, monthBs); // No need to cache this.
                }
                return (yearBs, monthBs, newDayBs);
            }
        }
    }
}
