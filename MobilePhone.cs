using System;
namespace Asset_tracking
{
    public class MobilePhone : Asset
    {
	    public MobilePhone(string brand, string modelName, DateTime purchaseDate, int price, string location)
	    {
            this = Asset(brand, modelName, purchaseDate, price, location);
        }
    }
}
