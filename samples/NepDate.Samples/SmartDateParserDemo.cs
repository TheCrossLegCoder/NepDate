using System;
using NepDate.Extensions;

namespace NepDate.Samples
{
    /// <summary>
    /// Demonstrates the usage of the SmartDateParser with various date formats.
    /// </summary>
    public static class SmartDateParserDemo
    {
        /// <summary>
        /// Runs the Smart Date Parser demonstration.
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("=== NepaliDate Smart Date Parser Demo ===\n");
            
            Console.WriteLine("The Smart Date Parser can parse dates in various formats, including:");
            Console.WriteLine("- Standard formats (YYYY/MM/DD, DD/MM/YYYY)");
            Console.WriteLine("- Formats with month names in English or Nepali");
            Console.WriteLine("- Nepali unicode digits");
            Console.WriteLine("- Mixed formats and ambiguous inputs\n");
            
            DemonstrateStandardFormats();
            DemonstrateMonthNameFormats();
            DemonstrateUnicodeFormats();
            DemonstrateMixedFormats();
            DemonstrateRobustness();
            DemonstrateInteractiveParser();
        }
        
        private static void DemonstrateStandardFormats()
        {
            Console.WriteLine("=== Standard Formats ===");
            
            TryParse("2080/04/15", "YYYY/MM/DD");
            TryParse("2080-04-15", "YYYY-MM-DD");
            TryParse("2080.04.15", "YYYY.MM.DD");
            TryParse("15/04/2080", "DD/MM/YYYY");
            TryParse("15-04-2080", "DD-MM-YYYY");
            TryParse("04/15/2080", "MM/DD/YYYY");
            
            Console.WriteLine();
        }
        
        private static void DemonstrateMonthNameFormats()
        {
            Console.WriteLine("=== Month Name Formats ===");
            
            TryParse("15 Shrawan 2080", "DD Month YYYY");
            TryParse("15 Sawan 2080", "DD Month YYYY (spelling variation)");
            TryParse("15 Saun 2080", "DD Month YYYY (another spelling variation)");
            TryParse("Shrawan 15, 2080", "Month DD, YYYY");
            TryParse("Shrawan 15 2080", "Month DD YYYY (no comma)");
            
            Console.WriteLine();
        }
        
        private static void DemonstrateUnicodeFormats()
        {
            Console.WriteLine("=== Unicode Formats ===");
            
            TryParse("२०८०/०४/१५", "YYYY/MM/DD in Nepali digits");
            TryParse("१५/०४/२०८०", "DD/MM/YYYY in Nepali digits");
            TryParse("१५ श्रावण २०८०", "DD Month YYYY in Nepali");
            TryParse("श्रावण १५, २०८०", "Month DD, YYYY in Nepali");
            
            Console.WriteLine();
        }
        
        private static void DemonstrateMixedFormats()
        {
            Console.WriteLine("=== Mixed Formats ===");
            
            TryParse("15 साउन 2080", "DD Nepali_Month English_Year");
            TryParse("साउन 15, २०८०", "Nepali_Month English_Day, Nepali_Year");
            TryParse("15 Shrawan २०८०", "English_Day English_Month Nepali_Year");
            
            Console.WriteLine();
        }
        
        private static void DemonstrateRobustness()
        {
            Console.WriteLine("=== Robustness Features ===");
            
            TryParse("15 Shrawan 2080 B.S.", "With B.S. suffix");
            TryParse("15 साउन 2080 BS", "With BS suffix");
            TryParse("15 Shrawan 2080 V.S.", "With V.S. suffix");
            TryParse("15 Srawan 2080", "With typo in month name");
            TryParse("15/4/80", "With short year and no leading zeros");
            TryParse("15 साउन, 2080 गते", "With 'gate' suffix");
            TryParse("15 साउन, 2080 मिति", "With 'miti' prefix");
            
            Console.WriteLine();
            
            Console.WriteLine("Invalid formats are handled gracefully:");
            TryParse("not a date", "Invalid input");
            TryParse("32/04/2080", "Invalid day");
            TryParse("15/13/2080", "Invalid month");
            
            Console.WriteLine();
        }
        
        private static void DemonstrateInteractiveParser()
        {
            Console.WriteLine("=== Interactive Date Parser ===");
            Console.WriteLine("Type any date format to parse it (or 'exit' to return to menu):");
            
            while (true)
            {
                Console.Write("\nEnter a date: ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input) || input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                
                try
                {
                    NepaliDate date = input.ToNepaliDate();
                    Console.WriteLine($"Parsed successfully: {date} (Day of week: {date.DayOfWeek})");
                    Console.WriteLine($"English equivalent: {date.EnglishDate.ToShortDateString()}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        
        private static void TryParse(string input, string description)
        {
            try
            {
                NepaliDate date = input.ToNepaliDate();
                Console.WriteLine($"✓ \"{input}\" ({description}) → {date}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ \"{input}\" ({description}) → Error: {ex.Message}");
            }
        }
    }
} 