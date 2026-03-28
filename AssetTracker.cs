using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Xml.Linq;


namespace Asset_tracking
{
    public class AssetTracker
    {
        public List<Asset> AssetsList { get; set; }
        private string FilePath { get; set; }
        private static int nextAssetID { get; set; }

        public AssetTracker()
        {
            AssetsList = new List<Asset>();

            FilePath = Environment.CurrentDirectory + "\\assets.json";

            CurrencyConverter.FetchCurrencyRatesFromECB(); // Fetches the currency exchange rates from ECB when AssetTracker is created.

            //If a file exists, load it. Otherwise, create a sample list of assets.
            if (File.Exists(FilePath))
                LoadAssetsFile();
            else
            {
                CreateSampleAssetsList();
                SaveFile();
            }
        }

        public void SaveFile()
        {
            //Tries to save the list in a JSON file
            try
            {
                string json = JsonSerializer.Serialize(AssetsList);
                File.WriteAllText(FilePath, json);
                // Console.WriteLine("Your list of assets have been saved");
            }
            catch (Exception)
            {
                ColoredText.WriteLine("Failed to save the list of assets!", ConsoleColor.Red);
            }
        }


        public void ShowAssets(string byTypeOrCountry)
        {
            if (byTypeOrCountry == "byType")
            {
                // sortAssetsListByTypeOfAsset();
                
                // I managed to replace the call to sortAssetsListByTypeOfAsset() with one line of code
                AssetsList = AssetsList.OrderBy(a => a.GetType().Name).ThenBy(a => a.PurchaseDate).ToList();
                ColoredText.WriteLine("\n Assets ordered by type of Asset and then by purchase date.", ConsoleColor.Yellow);
            }
            else if (byTypeOrCountry == "byCountry")
            {
                AssetsList = AssetsList.OrderBy(a => a.Country).ThenBy(a => a.PurchaseDate).ToList();
                ColoredText.WriteLine("\n Assets ordered by country and then by purchase date.", ConsoleColor.Yellow);
            }
        
            // Write header line.
            ColoredText.Write("\n Type of Asset".PadRight(19) + "Brand".PadRight(11) + "Model".PadRight(15), ConsoleColor.Green);
            ColoredText.Write("Country".PadRight(10) + "Purchase date".PadRight(16) + "Price in SEK".PadRight(15), ConsoleColor.Green);
            ColoredText.WriteLine("Currency".PadRight(11) + "Local price".PadRight(14), ConsoleColor.Green);

            foreach (Asset asset in AssetsList)
            {

                Console.Write("\n " + asset.GetType().Name.PadRight(17) + asset.Brand.PadRight(11) + asset.ModelName.PadRight(15) + asset.Country.PadRight(10));
                
                if (DateTime.Now.AddMonths(-36 + 3) >= asset.PurchaseDate)
                {
                    ColoredText.Write(asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(16), ConsoleColor.Red);
                }
                else if (DateTime.Now.AddMonths(-36 + 6) >= asset.PurchaseDate)
                {
                    ColoredText.Write(asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(16), ConsoleColor.Yellow);
                }
                else
                {
                    Console.Write(asset.PurchaseDate.ToString("yyyy-MM-dd").PadRight(16));
                }

                Console.WriteLine(asset.Price.ToString().PadRight(15) + asset.Currency.PadRight(11) + asset.LocalPrice.ToString().PadRight(14));
            }
        }
        
        public void AddNewAsset()
        {
            ColoredText.WriteLine("\n Enter a new asset.", ConsoleColor.Yellow);

            ColoredText.WriteLine("\n Start by entering the line number for the country where your office is. ", ConsoleColor.Yellow);
           

            Console.WriteLine("\n " + (1) + ". Germany");
            Console.WriteLine("\n " + (2) + ". Sweden");
            Console.WriteLine("\n " + (3) + ". Usa");

            int countryIndex = GetValidatedIntFromConsole("Line number", 1, 3);

            string country = "Usa";
            string currency = "USD";

            if (countryIndex == 1) {
                country = "Germany";
                currency = "EUR";
            }
            else if (countryIndex == 2)
            {
                country = "Sweden";
                currency = "SEK";
            }

            ColoredText.WriteLine("\n Enter the type of asset (Computer, MobilePhone etc)", ConsoleColor.Yellow);

            Console.WriteLine("\n " + (1) + ". Computer");
            Console.WriteLine("\n " + (2) + ". Mobile phone");

            int typeIndex = GetValidatedIntFromConsole("Line number", 1, 2); 

            ColoredText.Write("\n Enter the brand name of the asset. ", ConsoleColor.Yellow);
            string brand = GetValidatedStringFromConsole("Brand name");

            ColoredText.Write("\n Enter the model name of the asset. ", ConsoleColor.Yellow);
            string model = GetValidatedStringFromConsole("Model name");

            ColoredText.Write("\n Enter the purchase date: ", ConsoleColor.Yellow);
            DateTime purchaseDate = GetValidatedDateFromConsole();

            ColoredText.WriteLine("\n Enter the price of the product in swedish krona. ", ConsoleColor.Yellow);
            decimal price = GetValidatedDecimalFromConsole("Price");

            Asset asset;
            if (typeIndex == 1 )
                asset = new Computer(nextAssetID++, brand, model, purchaseDate, price, currency, country);
            else  // typeIndex == 2
                asset = new MobilePhone(nextAssetID++, brand, model, purchaseDate, price, currency, country);
    
            AssetsList.Add(asset);

            ColoredText.WriteLine("\n A new asset has been added to the assets list", ConsoleColor.Yellow);
        }

        public void RemoveAsset()
        {
            ColoredText.WriteLine("\n Enter the line number for the asset you want to remove", ConsoleColor.Yellow);
            ShowAssetsWithLineNumbers();

            int index = GetValidatedIntFromConsole("Line number", 1 , AssetsList.Count);

            ColoredText.WriteLine("\n The selected asset has been removed", ConsoleColor.Yellow);
            AssetsList.RemoveAt(index - 1);
        }

        public int GetValidatedIntFromConsole(string variableName, int min, int max)
        {
            bool isValidInteger = false;
            int index;
            do
            {
                ColoredText.Write("\n Enter a " + variableName + ": ", ConsoleColor.Yellow);
                isValidInteger = int.TryParse(Console.ReadLine(), out index);

                if (isValidInteger == false)
                {
                    ColoredText.WriteLine("\n " + variableName + " can only contain digits and can't be empty.", ConsoleColor.Red);
                }
                else if (index < min || index > max)
                {
                    ColoredText.WriteLine("\n " + variableName + " must be non-negative and higher than zero and lower than " + (max + 1) + ".", ConsoleColor.Red);
                    isValidInteger = false;
                }
            } while (isValidInteger == false);

            return index;
        }

        private void ShowAssetsWithLineNumbers()
        {
            AssetsList = AssetsList.OrderBy(a => a.GetType().Name).ThenBy(a => a.PurchaseDate).ToList();

            // Write header line.
            ColoredText.Write("\n Type of Asset".PadRight(21) + "Brand".PadRight(11) + "Model".PadRight(15), ConsoleColor.Green);
            ColoredText.Write("Country".PadRight(10) + "Purchase date".PadRight(16) + "Price in SEK".PadRight(15), ConsoleColor.Green);
            ColoredText.WriteLine("Currency".PadRight(11) + "Local price".PadRight(14), ConsoleColor.Green);

            for (int i = 0; i < AssetsList.Count; i++)
            {
                if (i < 9)
                    Console.Write("\n  " + (i + 1) + ". " + AssetsList[i].GetType().Name.PadRight(15) + AssetsList[i].Brand.PadRight(11));
                else
                    Console.Write("\n " + (i + 1) + ". " + AssetsList[i].GetType().Name.PadRight(15) + AssetsList[i].Brand.PadRight(11));

                Console.Write(AssetsList[i].ModelName.PadRight(15) + AssetsList[i].Country.PadRight(10) + AssetsList[i].PurchaseDate.ToString("yyyy-MM-dd").PadRight(16));
                Console.WriteLine(AssetsList[i].Price.ToString().PadRight(15) + AssetsList[i].Currency.PadRight(11) + AssetsList[i].LocalPrice.ToString().PadRight(14));
            }
        }

        // Gets a atring from the console and validates it so its not empty.
        private string GetValidatedStringFromConsole(string variableName)
        {
            string result = Console.ReadLine();

            while (String.IsNullOrEmpty(result))
            {
                ColoredText.WriteLine("\n " + variableName + " can't be an empty string", ConsoleColor.Red);

                ColoredText.Write("\n Enter a " + variableName + ": ", ConsoleColor.Yellow);
                result = Console.ReadLine();
            }

            return result;
        }

       
        private decimal GetValidatedDecimalFromConsole(string variableName)
        {
            bool isDecimal = false;
            decimal value;

            do
            {
                ColoredText.Write("\n Enter a " + variableName + ": ", ConsoleColor.Yellow);
                isDecimal = decimal.TryParse(Console.ReadLine(), out value);

                if (isDecimal == false)
                {
                    ColoredText.WriteLine("\n " + variableName + " can only contain digits and can't be empty.", ConsoleColor.Red);
                }
                else if (value < 1)
                {
                    ColoredText.WriteLine("\n " + variableName + " must be higher than zero.", ConsoleColor.Red);
                    isDecimal = false;
                }


            } while (isDecimal == false);

                return Math.Round(value);
        }

        // Thera are two cases, in the first case NullOrEmpty is allowed, in the second case its treated as an error.
        private DateTime GetValidatedDateFromConsole()
        {
            bool isDate;
            DateTime date;
            
            isDate = DateTime.TryParse(Console.ReadLine(), out date);
            
            while (isDate == false)
            {
                ColoredText.WriteLine("\n You have not entered a valid date.", ConsoleColor.Red);
               
                ColoredText.Write("\n Enter a valid date: ", ConsoleColor.Yellow);
                isDate = DateTime.TryParse(Console.ReadLine(), out date);
            } 

            return date;
        }


        private void LoadAssetsFile()
        {
            try
            {
                // Read the entire content of the JSON file into a string
                var json = File.ReadAllText(FilePath);
                AssetsList = JsonSerializer.Deserialize<List<Asset>>(json);

                nextAssetID = AssetsList[AssetsList.Count - 1].ID + 1;
            }
            catch (Exception)
            {
                ColoredText.WriteLine(" Failed to open saved Projects List\n", ConsoleColor.Red);
            }
        }

        private void CreateSampleAssetsList()
        {
            nextAssetID = 0;

            AssetsList.AddRange(
                [
                    new MobilePhone(nextAssetID++, "Motorola", "X3", DateTime.Now.AddMonths(-36 + 4), 1880, "USD", "Usa"),
                    new MobilePhone(nextAssetID++, "Motorola", "X3", DateTime.Now.AddMonths(-36 + 5), 3750, "USD", "Usa"),
                    new MobilePhone(nextAssetID++, "Motorola", "X2", DateTime.Now.AddMonths(-36 + 10), 3750, "USD", "Usa"),
                    new MobilePhone(nextAssetID++, "Samsung", "Galaxy 10", DateTime.Now.AddMonths(-36 + 6), 4500, "SEK", "Sweden"),
                    new MobilePhone(nextAssetID++, "Samsung", "Galaxy 10", DateTime.Now.AddMonths(-36 + 7), 4500, "SEK", "Sweden"),
                    new MobilePhone(nextAssetID++, "Sony", "XPeria 7", DateTime.Now.AddMonths(-36 + 4), 3000, "SEK", "Sweden"),
                    new MobilePhone(nextAssetID++, "Sony", "XPeria 7", DateTime.Now.AddMonths(-36 + 5), 3000, "SEK", "Sweden"),
                    new MobilePhone(nextAssetID++, "Siemens", "Brick", DateTime.Now.AddMonths(-36 + 12), 2385, "EUR", "Germany"),
                    new Computer(nextAssetID++, "Dell", "Desktop 900", DateTime.Now.AddMonths(-38), 940, "USD", "Usa"),
                    new Computer(nextAssetID++, "Dell", "Desktop 900", DateTime.Now.AddMonths(-37), 940, "USD", "Usa"),
                    new Computer(nextAssetID++, "Lenovo", "X100", DateTime.Now.AddMonths(-36 + 1), 2800, "USD", "Usa"),
                    new Computer(nextAssetID++, "Lenovo", "X200", DateTime.Now.AddMonths(-36 + 4), 2800, "USD", "Usa"),
                    new Computer(nextAssetID++, "Lenovo", "X300", DateTime.Now.AddMonths(-36 + 9), 4690, "USD", "Usa"),
                    new Computer(nextAssetID++, "Dell", "Optiplex 100", DateTime.Now.AddMonths(-36 + 7), 1500, "SEK", "Sweden"),
                    new Computer(nextAssetID++, "Dell", "Optiplex 200", DateTime.Now.AddMonths(-36 + 8), 1400, "SEK", "Sweden"),
                    new Computer(nextAssetID++, "Dell", "Optiplex 300", DateTime.Now.AddMonths(-36 + 9), 1300, "SEK", "Sweden"),
                    new Computer(nextAssetID++, "Asus", "ROG 600", DateTime.Now.AddMonths(-36 + 14), 17350, "EUR", "Germany"),
                    new Computer(nextAssetID++, "Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 4), 13000, "EUR", "Germany"),
                    new Computer(nextAssetID++, "Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 3), 13000, "EUR", "Germany"),
                    new Computer(nextAssetID++, "Asus", "ROG 500", DateTime.Now.AddMonths(-36 + 2), 14000, "EUR", "Germany")
                ]
            );
        }
        // Not used anymore.
        private List<Asset> sortAssetsListByTypeOfAsset()
        {
            List<Asset> computerAssets = new List<Asset>();
            List<Asset> mobilePhoneAssets = new List<Asset>();

            foreach (Asset asset in AssetsList)
            {
                if (asset.GetType().Name == "Computer")
                {
                    computerAssets.Add(asset);
                }
                else if (asset.GetType().Name == "MobilePhone")
                {
                    mobilePhoneAssets.Add(asset);
                }
            }

            computerAssets = computerAssets.OrderBy(a => a.PurchaseDate).ToList();
            mobilePhoneAssets = mobilePhoneAssets.OrderBy(a => a.PurchaseDate).ToList();

            AssetsList.Clear();

            AssetsList.AddRange(computerAssets);
            AssetsList.AddRange(mobilePhoneAssets);

            return AssetsList;
        }
    }
}