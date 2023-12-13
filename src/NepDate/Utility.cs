using NepDate.Core.Constants;
using NepDate.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NepDate
{
    public partial struct NepaliDate
    {
        /// <summary>
        /// Gets the current/today Nepali date.
        /// </summary>
        public static NepaliDate Now => DateTime.Now.ToNepaliDate();

        /// <summary>
        /// Min year value
        /// </summary>
        public static readonly int MinYear = NepDateConstants._minYear;

        /// <summary>
        /// Max year value
        /// </summary>
        public static readonly int MaxYear = NepDateConstants._maxYear;

        /// <summary>
        /// Represents the smallest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MinValue = Parse($"{NepDateConstants._minYear}/01/01");

        /// <summary>
        /// Represents the largest possible value of a Nepali date.
        /// </summary>
        public static readonly NepaliDate MaxValue = Parse($"{NepDateConstants._maxYear}/12/30");
    }
}
