using System;

namespace NepDate.Samples
{
    /// <summary>
    /// Contains the main entry point for the samples console application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the samples console application.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("NepDate Samples");
            Console.WriteLine("==============");
            Console.WriteLine();

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Choose a sample to run:");
            Console.WriteLine("1) Date Difference Calculator Demo");
            Console.WriteLine("2) Serialization Demo");
            Console.WriteLine("3) Smart Date Parser Demo");
            Console.WriteLine("0) Exit");
            Console.Write("\r\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    DateDifferenceDemo.Run();
                    Console.WriteLine("\r\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "2":
                    Console.Clear();
                    SerializationDemo.Run();
                    Console.WriteLine("\r\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "3":
                    Console.Clear();
                    SmartDateParserDemo.Run();
                    Console.WriteLine("\r\nPress any key to return to the main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return true;
                case "0":
                    return false;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                    return true;
            }
        }
    }
} 