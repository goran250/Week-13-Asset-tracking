using System;

namespace Asset_tracking
{

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