using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NepDate
{
    public readonly partial struct NepaliDate
    {
        /// <summary>
        /// Provides methods for bulk conversion between English and Nepali dates.
        /// </summary>
        public class BulkConvert
        {
            // Threshold for using parallel processing
            private const int ParallelThreshold = 500;

            /// <summary>
            /// Converts a collection of English dates to Nepali dates.
            /// </summary>
            /// <param name="engDates">The collection of English dates to convert.</param>
            /// <param name="useParallel">Whether to use parallel processing for large collections.</param>
            /// <returns>An IEnumerable of NepaliDate corresponding to the input English dates.</returns>
            public static IEnumerable<NepaliDate> ToNepaliDates(IEnumerable<DateTime> engDates, bool useParallel = true)
            {
                var datesList = engDates.ToList();
                
                if (useParallel && datesList.Count > ParallelThreshold)
                {
                    return datesList.AsParallel().Select(item => new NepaliDate(item)).ToList();
                }
                
                return datesList.Select(item => new NepaliDate(item));
            }


            /// <summary>
            /// Converts a collection of Nepali dates represented as strings to English dates.
            /// </summary>
            /// <param name="nepDates">The collection of Nepali dates (as strings) to convert.</param>
            /// <param name="useParallel">Whether to use parallel processing for large collections.</param>
            /// <returns>An IEnumerable of DateTime representing the English dates corresponding to the input Nepali dates.</returns>
            public static IEnumerable<DateTime> ToEnglishDates(IEnumerable<string> nepDates, bool useParallel = true)
            {
                var datesList = nepDates.ToList();
                
                if (useParallel && datesList.Count > ParallelThreshold)
                {
                    return datesList.AsParallel().Select(item => Parse(item).EnglishDate).ToList();
                }
                
                return datesList.Select(item => Parse(item).EnglishDate);
            }


            /// <summary>
            /// Converts a collection of Nepali dates to English dates.
            /// </summary>
            /// <param name="nepDates">The collection of Nepali dates to convert.</param>
            /// <param name="useParallel">Whether to use parallel processing for large collections.</param>
            /// <returns>An IEnumerable of DateTime representing the English dates corresponding to the input Nepali dates.</returns>
            public static IEnumerable<DateTime> ToEnglishDates(IEnumerable<NepaliDate> nepDates, bool useParallel = true)
            {
                var datesList = nepDates.ToList();
                
                if (useParallel && datesList.Count > ParallelThreshold)
                {
                    return datesList.AsParallel().Select(item => item.EnglishDate).ToList();
                }
                
                return datesList.Select(item => item.EnglishDate);
            }
            
            /// <summary>
            /// Batch processes a large collection of dates for conversion.
            /// </summary>
            /// <param name="engDates">The collection of English dates to convert.</param>
            /// <param name="batchSize">The size of each batch to process.</param>
            /// <returns>An IEnumerable of NepaliDate corresponding to the input English dates.</returns>
            public static IEnumerable<NepaliDate> BatchProcessToNepaliDates(IEnumerable<DateTime> engDates, int batchSize = 1000)
            {
                var result = new List<NepaliDate>();
                var batch = new List<DateTime>(batchSize);
                
                foreach (var date in engDates)
                {
                    batch.Add(date);
                    
                    if (batch.Count >= batchSize)
                    {
                        result.AddRange(ToNepaliDates(batch, true));
                        batch.Clear();
                    }
                }
                
                if (batch.Count > 0)
                {
                    result.AddRange(ToNepaliDates(batch, true));
                }
                
                return result;
            }
            
            /// <summary>
            /// Batch processes a large collection of Nepali dates for conversion.
            /// </summary>
            /// <param name="nepDates">The collection of Nepali dates to convert.</param>
            /// <param name="batchSize">The size of each batch to process.</param>
            /// <returns>An IEnumerable of DateTime corresponding to the input Nepali dates.</returns>
            public static IEnumerable<DateTime> BatchProcessToEnglishDates(IEnumerable<NepaliDate> nepDates, int batchSize = 1000)
            {
                var result = new List<DateTime>();
                var batch = new List<NepaliDate>(batchSize);
                
                foreach (var date in nepDates)
                {
                    batch.Add(date);
                    
                    if (batch.Count >= batchSize)
                    {
                        result.AddRange(ToEnglishDates(batch, true));
                        batch.Clear();
                    }
                }
                
                if (batch.Count > 0)
                {
                    result.AddRange(ToEnglishDates(batch, true));
                }
                
                return result;
            }
        }
    }
}
