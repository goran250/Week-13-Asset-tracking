using System;
namespace Asset_tracking
{
    public class Asset
    {
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }


        public Asset(string brand, string modelName, DateTime purchaseDate, int price, string location)
        {
            Brand = brand;
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
            Location = location;
        }
    }
}