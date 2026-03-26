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
            Console.WriteLine("Hello, World!");

            tracker = new AssetTracker();

            addAssets();
        }

        public static void addAssets()
        {
            
            tracker.AddAsset(new MobilePhone("Motorola", "X3", DateTime.Now.AddMonths(-36 + 4), new Price(200, Currency.USD), usa));
            tracker.AddAsset(new MobilePhone("Motorola", "X3", DateTime.Now.AddMonths(-36 + 5), new Price(400, Currency.USD)usa));
            tracker.AddAsset(new MobilePhone("Motorola", "X2", DateTime.Now.AddMonths(-36 + 10), new Price(400, Currency.USD), usa));
            tracker.AddAsset(new MobilePhone("Samsung", "Galaxy 10", DateTime.Now.AddMonths(-36 + 6), new Price(4500, Currency.SEK), sweden));
            tracker.AddAsset(new MobilePhone("Samsung", "Galaxy 10", DateTime.Now.AddMonths(-36 + 7), new Price(4500, Currency.SEK), sweden));
            tracker.AddAsset(new MobilePhone("Sony", "XPeria 7", DateTime.Now.AddMonths(-36 + 4), new Price(3000, Currency.SEK), sweden));
            tracker.AddAsset(new MobilePhone("Sony", "XPeria 7", DateTime.Now.AddMonths(-36 + 5), new Price(3000, Currency.SEK), sweden));
            tracker.AddAsset(new MobilePhone("Siemens", "Brick", DateTime.Now.AddMonths(-36 + 12), new Price(220, Currency.EUR), germany));
            tracker.AddAsset(new Computer("Dell", "Desktop 900", DateTime.Now.AddMonths(-38), new Price(100, Currency.USD), usa));
            tracker.AddAsset(new Computer("Dell", "Desktop 900", DateTime.Now.AddMonths(-37), new Price(100, Currency.USD), usa));
            tracker.AddAsset(new Computer("Lenovo", "X100", DateTime.Now.AddMonths(-36 + 1), new Price(300, Currency.USD), usa));
            tracker.AddAsset(new Computer("Lenovo", "X200", DateTime.Now.AddMonths(-36 + 4), new Price(300, Currency.USD), usa));
            tracker.AddAsset(new Computer("Lenovo", "X300", DateTime.Now.AddMonths(-36 + 9), new Price(500, Currency.USD), usa));
            tracker.AddAsset(new Computer("Dell", "Optiplex 100", DateTime.Now.AddMonths(-36 + 7), new Price(1500, Currency.SEK), sweden));
            tracker.AddAsset(new Computer("Dell", "Optiplex 200", DateTime.Now.AddMonths(-36 + 8), new Price(1400, Currency.SEK), sweden));
            tracker.AddAsset(new Computer("Dell", "Optiplex 300", DateTime.Now.AddMonths(-36 + 9), new Price(1300, Currency.SEK), sweden));
            tracker.AddAsset(new Computer("Asus", "ROG 600", DateTime.Now.AddMonths(-36 + 14), new Price(1600, Currency.EUR), germany));
            tracker.AddAsset(new Computer("Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 4), new Price(1200, Currency.EUR), germany));
            tracker.AddAsset(new Computer("Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 3), new Price(1200, Currency.EUR), germany));
            tracker.AddAsset(new Computer("Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 2), new Price(1300, Currency.EUR), germany));

        }
    }
