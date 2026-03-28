using System;
namespace Asset_tracking
{
    public class MobilePhone : Asset
    {
        public MobilePhone(int id, string brand, string modelName, DateTime purchaseDate, decimal price, string currency, string country)
	    {       
            ID = id;
            Brand = brand;
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
            Currency = currency;
            Country = country;
            LocalPrice = CurrencyConverter.GetLocalPrice(price, currency);
        }
    }
}
