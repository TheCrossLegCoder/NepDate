using System;
using System.Collections.Generic;
using System.Text;

namespace NepDate
{
    public readonly partial struct NepaliDate : IEquatable<NepaliDate>
    {
        /// <summary>
        /// Determines whether this NepaliDate instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the specified object is a NepaliDate and has the same value as the current instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is NepaliDate date && Equals(date);
        }
    }
}
