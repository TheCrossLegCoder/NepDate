using System;
using System.Diagnostics;
using NepDate;

namespace NepDate.MemoryBenchmark
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NepDate Memory Usage Benchmark");
            Console.WriteLine("==============================");
            Console.WriteLine();

            int numIterations = 10000;
            if (args.Length > 0 && int.TryParse(args[0], out int parsedValue))
            {
                numIterations = parsedValue;
            }

            Console.WriteLine($"Running with {numIterations} iterations");
            Console.WriteLine();

            // Single conversion benchmark
            MeasureSingleConversion();

            // Multiple conversion benchmark
            MeasureMultipleConversions(numIterations);

            // Memory usage by cache
            MeasureCacheImpact(numIterations / 10);

            Console.WriteLine("\nBenchmark complete. Press any key to exit.");
            Console.ReadKey();
        }

        private static void MeasureSingleConversion()
        {
            Console.WriteLine("1. SINGLE CONVERSION MEMORY USAGE");
            Console.WriteLine("=================================");

            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Measure baseline
            long memoryBefore = GC.GetTotalMemory(true);

            // Eng to Nep
            var engDate = new DateTime(2023, 9, 1);
            var nepDate = new NepaliDate(engDate);

            // Measure after Eng to Nep
            long memoryAfterToNep = GC.GetTotalMemory(true);

            // Nep to Eng
            var backToEng = nepDate.EnglishDate;

            // Measure after Nep to Eng
            long memoryAfterToEng = GC.GetTotalMemory(true);

            // Output results
            Console.WriteLine($"Memory for English to Nepali conversion: {memoryAfterToNep - memoryBefore:N0} bytes");
            Console.WriteLine($"Memory for Nepali to English conversion: {memoryAfterToEng - memoryAfterToNep:N0} bytes");
            Console.WriteLine($"Total memory for both conversions: {memoryAfterToEng - memoryBefore:N0} bytes");
            Console.WriteLine($"NepaliDate struct approx. size: {sizeof(int) * 3} bytes (just Year, Month, Day)");
            Console.WriteLine();
        }

        private static void MeasureMultipleConversions(int iterations)
        {
            Console.WriteLine("2. BATCH CONVERSION MEMORY USAGE");
            Console.WriteLine("================================");

            // Force garbage collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Measure baseline
            long memoryBefore = GC.GetTotalMemory(true);
            var stopwatch = Stopwatch.StartNew();

            // Keep references to prevent GC during measurement
            var nepaliDates = new NepaliDate[iterations];
            var backToEnglish = new DateTime[iterations];

            // Perform conversions
            for (int i = 0; i < iterations; i++)
            {
                // Use different dates to avoid excessive caching
                var engDate = new DateTime(2020, 1, 1).AddDays(i % 366);
                nepaliDates[i] = new NepaliDate(engDate);
                backToEnglish[i] = nepaliDates[i].EnglishDate;
            }

            stopwatch.Stop();
            long memoryAfter = GC.GetTotalMemory(true);

            // Calculate and display results
            long totalMemory = memoryAfter - memoryBefore;
            double memoryPerConversion = (double)totalMemory / iterations;
            double timePerConversion = stopwatch.Elapsed.TotalMilliseconds / iterations;

            Console.WriteLine($"Total memory used: {totalMemory:N0} bytes");
            Console.WriteLine($"Memory per conversion pair: {memoryPerConversion:F2} bytes");
            Console.WriteLine($"Total time: {stopwatch.Elapsed.TotalMilliseconds:N2} ms");
            Console.WriteLine($"Time per conversion pair: {timePerConversion:F6} ms");
            Console.WriteLine();
        }

        private static void MeasureCacheImpact(int iterations)
        {
            Console.WriteLine("3. CACHING IMPACT ANALYSIS");
            Console.WriteLine("==========================");

            // First, repeated conversion of same date (should benefit from cache)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBeforeRepeated = GC.GetTotalMemory(true);
            var stopwatchRepeated = Stopwatch.StartNew();

            var engDate = new DateTime(2023, 9, 1);
            for (int i = 0; i < iterations; i++)
            {
                var nepDate = new NepaliDate(engDate);
                var result = nepDate.EnglishDate;
                
                // Ensure value is used
                if (result.Year < 2000) Console.WriteLine("Unexpected year");
            }

            stopwatchRepeated.Stop();
            long memoryAfterRepeated = GC.GetTotalMemory(true);

            // Now, conversion of unique dates (less cache benefit)
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryBeforeUnique = GC.GetTotalMemory(true);
            var stopwatchUnique = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                var uniqueDate = new DateTime(2020, 1, 1).AddDays(i);
                var nepDate = new NepaliDate(uniqueDate);
                var result = nepDate.EnglishDate;
                
                // Ensure value is used
                if (result.Year < 2000) Console.WriteLine("Unexpected year");
            }

            stopwatchUnique.Stop();
            long memoryAfterUnique = GC.GetTotalMemory(true);

            // Calculate and display results
            long memoryRepeated = memoryAfterRepeated - memoryBeforeRepeated;
            long memoryUnique = memoryAfterUnique - memoryBeforeUnique;
            double timeRepeated = stopwatchRepeated.Elapsed.TotalMilliseconds;
            double timeUnique = stopwatchUnique.Elapsed.TotalMilliseconds;

            Console.WriteLine("Comparing repeated dates vs unique dates:");
            Console.WriteLine($"Memory for {iterations} repeated conversions: {memoryRepeated:N0} bytes ({memoryRepeated / (double)iterations:F2} bytes/conversion)");
            Console.WriteLine($"Memory for {iterations} unique conversions: {memoryUnique:N0} bytes ({memoryUnique / (double)iterations:F2} bytes/conversion)");
            Console.WriteLine($"Memory saved by caching: {memoryUnique - memoryRepeated:N0} bytes");
            Console.WriteLine();
            Console.WriteLine($"Time for repeated conversions: {timeRepeated:N2} ms ({timeRepeated / iterations:F6} ms/conversion)");
            Console.WriteLine($"Time for unique conversions: {timeUnique:N2} ms ({timeUnique / iterations:F6} ms/conversion)");
            Console.WriteLine($"Time saved by caching: {timeUnique - timeRepeated:N2} ms");
            Console.WriteLine();
        }
    }
} 