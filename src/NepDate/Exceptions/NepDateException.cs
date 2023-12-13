using System;

namespace NepDate.Exceptions
{
    public sealed class NepDateException
    {
        public sealed class InvalidNepaliDateFormatException : FormatException
        {
            public InvalidNepaliDateFormatException(string message = "Invalid Nepali date format") : base(message) { }
        }

        public sealed class InvalidNepaliDateArgumentException : ArgumentException
        {
            public InvalidNepaliDateArgumentException(string message = "Provided Nepali date is empty") : base(message) { }
        }
    }
}
