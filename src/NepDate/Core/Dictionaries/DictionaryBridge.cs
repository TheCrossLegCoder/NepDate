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
                if (NepaliToEnglish.data.TryGetValue(((ushort)nepYear, (byte)nepMonth), out (byte NepMonthEndDay, ushort EngYear, byte EngMonth, byte EngDay) dictVal))
                {
                    int dateDiff = nepDay - dictVal.NepMonthEndDay;
                    return new DateTime(dictVal.EngYear, dictVal.EngMonth, dictVal.EngDay).AddDays(dateDiff);
                }

                throw new InvalidNepaliDateArgumentException();
            }

            internal static int GetNepaliMonthEndDay(int nepYear, int nepMonth)
            {
                if (NepaliToEnglish.data.TryGetValue(((ushort)nepYear, (byte)nepMonth), out var dictVal))
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
                _ = EnglishToNepali.data.TryGetValue(((ushort)engYear, (byte)engMonth), out (byte EngMonthEndDay, ushort NepYear, byte NepMonth, byte NepDay) dictVal);

                byte dateDiff = (byte)(dictVal.EngMonthEndDay - engDay);

                return SubtractNepaliDays(dictVal.NepYear, dictVal.NepMonth, dictVal.NepDay, dateDiff);
            }

            private static (ushort yearBs, byte monthBs, byte dayBs) SubtractNepaliDays(ushort yearBs, byte monthBs, byte dayBs, int daysToSubtract)
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
                return (yearBs, monthBs, (byte)newDayBs);
            }
        }
    }
}
