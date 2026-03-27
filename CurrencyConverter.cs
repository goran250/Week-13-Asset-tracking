using Asset_tracking;
using System;
using System.Xml;
namespace Asset_tracking
{
	
	public class CurrencyConverter
	{
        private static List<Currency> CurrencyList;

        // Converts swedish krona to dollar or euro
        public static decimal GetLocalPrice(decimal price, string currency)
		{
            decimal eurUsdRate = getLocalCurrencyRate("USD");
            decimal eurSekRate = getLocalCurrencyRate("SEK"); 

            decimal localPrice = Convert.ToDecimal(price);

            if (currency == "USD")
            {
                localPrice = price / eurSekRate; // Converting SEK to EUR 
                localPrice = localPrice * eurUsdRate;               // Converting Eur to USD
            }
            else if (currency == "EUR")
            {
                localPrice = price / eurSekRate; // Converting SEK to EUR 

            }
			return Math.Round(localPrice);
        }

        private static decimal getLocalCurrencyRate(string localCurrencyCode)
        {
            foreach (Currency currency in CurrencyList)
            {
                if (localCurrencyCode == currency.CurrencyCode)
                {
                    return currency.ExchangeRate;
                }
            }

            return 1;
        }

        public static void FetchCurrencyRatesFromECB()
        {
            CurrencyList = new List<Currency>();
            
            string url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"; // Exchange rate XML document

            XmlTextReader reader = new XmlTextReader(url);
            while (reader.Read()) // Goes through the XML document and saves the currency exchange rates to the local list
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        if (reader.Name == "currency") // Identifies each currency attribute and saves the currency code and rate as an object
                        {
                            string currencyCode = reader.Value;                        
                            reader.MoveToNextAttribute();
                            string value = reader.Value;                  
                            CurrencyList.Add(new Currency(currencyCode, Convert.ToDecimal(value.Replace(".", ","))));
                        }
                    }
                }
            }
        }
    }

    public class Currency
    {
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }

        public Currency(string currencyCode, decimal exchangeRate)
        {
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
        }
    }


}

