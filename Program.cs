using System.Collections;
using System.Diagnostics;
using System.IO.Pipelines;

namespace Asset_tracking
{
    internal class Program
    {
        public static AssetTracker tracker;

        static void Main(string[] args)
        {
            tracker = new AssetTracker();

            Console.SetWindowSize(120, 65);

          

            ColoredText.WriteLine("\n Welcome to this Asset-tracker app!", ConsoleColor.Yellow);

            ShowMenu();
        }

        private static void ShowMenu()
        {
            ColoredText.WriteLine("\n Pick an option:", ConsoleColor.Yellow);

            Console.Write("\n (");
            ColoredText.Write("1", ConsoleColor.Yellow);
            Console.Write(") Show assets ordered by type of Asset and then by purchase date.");


            Console.Write("\n (");
            ColoredText.Write("2", ConsoleColor.Yellow);
            Console.Write(") Show assets ordered by country and then by purchase date.");

            Console.Write("\n (");
            ColoredText.Write("3", ConsoleColor.Yellow);
            Console.Write(") Add a new asset");

            Console.Write("\n (");
            ColoredText.Write("4", ConsoleColor.Yellow);
            Console.Write(") Remove an asset");

            Console.Write("\n (");
            ColoredText.Write("5", ConsoleColor.Yellow);
            Console.Write(") Save and Quit.");

            Navigate();
        }
        private static void Navigate()
        {
            int min = 1;
            int max = 5;     
            Console.WriteLine();
            int answer = tracker.GetValidatedIntFromConsole("Line number", min, max);

            switch (answer)
            {
                case 1:
                    tracker.ShowAssets("byType");
                    break;
                case 2:
                    tracker.ShowAssets("byCountry");
                    break;
                case 3:
                    tracker.AddNewAsset();
                    break;
                case 4:
                    tracker.RemoveAsset();
                    break;
                case 5:
                    tracker.SaveFile();
                    System.Environment.Exit(0);
                    break;
              
            }

            ShowMenu();
        }

      
    }
}

