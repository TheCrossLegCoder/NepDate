using System;
using System.Collections.Generic;
using System.Text;

namespace NepDate.Core.Dictionaries
{
    internal class NepaliMonths
    {
        public static IDictionary<int, string> Months = new Dictionary<int, string>()
        {
            {1, "Baishakh"},
            {2, "Jestha"},
            {3, "Ashad"},
            {4, "Shrawan"},
            {5, "Bhadra"},
            {6, "Ashoj"},
            {7, "Kartik"},
            {8, "Mangsir"},
            {9, "Poush"},
            {10, "Magh"},
            {11, "Falgun"},
            {12, "Chaitra"}
        };
    }
}
