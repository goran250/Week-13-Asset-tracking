using System;
namespace Asset_tracking
{
	public class Computer : Asset
	{
		public Computer(int id, string brand, string modelName, DateTime purchaseDate, decimal price, string currency, string location)
		{
            ID = id;
            Brand = brand;
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
            Currency = currency;
            Location = location;
            LocalPrice = CurrencyConverter.GetLocalPrice(price, currency);
        }

    }
}