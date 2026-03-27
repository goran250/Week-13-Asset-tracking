using System;
using System.Text.Json.Serialization;
namespace Asset_tracking
{
    [JsonDerivedType(typeof(Asset), typeDiscriminator: "Asset")]
    [JsonDerivedType(typeof(Computer), typeDiscriminator: "Computer")]
    [JsonDerivedType(typeof(MobilePhone), typeDiscriminator: "(MobilePhone")]
    public class Asset
    {
        public int ID { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Location { get; set; }

        public decimal LocalPrice { get; set; }

        public Asset(){ }

        public Asset(int id, string brand, string modelName, DateTime purchaseDate, int price, string currency,  string location)
        {
            ID = id;
            Brand = brand;
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
            Currency = currency;
            Location = location;      
            LocalPrice = CurrencyConverter.GetLocalPrice(Price, Currency);
        }
    }
}