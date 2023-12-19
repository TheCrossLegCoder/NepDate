using System;
using System.Collections.Generic;
using System.Text;

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
