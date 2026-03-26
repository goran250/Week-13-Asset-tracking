using System;
namespace Asset_tracking
{
	public class Computer : Asset
	{
		public Computer(string brand, string modelName, DateTime purchaseDate, int price, string location)
		{
			Asset(brand, modelName, purchaseDate, price, location);
		}
	}
}