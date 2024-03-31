using System;

namespace NepDate
{
    public readonly partial struct NepaliDate : IComparable<NepaliDate>
    {
        public int CompareTo(NepaliDate obj)
        {
            return obj.AsInteger.CompareTo(AsInteger);
        }
    }
}
