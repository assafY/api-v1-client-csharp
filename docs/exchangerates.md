##`Info.Blockchain.Api.ExchangeRates` namespace

The `Exchangerates` namespace contains the `ExchangeRateExplorer` class that reflects the functionality documented at https://blockchain.info/api/exchange_rates_api. It allows users to get price tickers for most major currencies and directly convert fiat amounts to BTC.

Example usage:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using Info.Blockchain.Api.Clieny;
using Info.Blockchain.Api.ExchangeRates;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            var explorer = new ExchangeRateExplorer();
            try
            {
                var ticker = await explorer.GetTickerAsync();
                foreach (var key in ticker.Keys)    
                {
                    Console.WriteLine("The last price of BTC in {0} is {1}", key, ticker[key].Last);
                }

                double btcAmount = await explorer.ToBtcAsync("USD", 1500);
                Console.WriteLine("1500 USD equals {0} BTC", btcAmount);
            }
            catch (APIException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}

```
