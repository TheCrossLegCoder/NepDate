using System;

namespace NepDate
{
    public static class Extensions
    {
        public static NepaliDate ToNepaliDate(this DateTime englishDate)
        {
            return new NepaliDate(englishDate);
        }
    }
}
