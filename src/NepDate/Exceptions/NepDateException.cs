using System;

namespace NepDate.Exceptions
{
    public sealed class NepDateException
    {
        public sealed class InvalidNepaliDateFormatException : FormatException
        {
            public InvalidNepaliDateFormatException(string message = "Invalid Nepali date format") : base(message) { }
        }
    }
}
