## `Info.Blockchain.Api.Statistics` namespace

The `Statistics` namespace contains the `StatisticsExplorer` class that reflects the functionality documented at at https://blockchain.info/api/charts_api. It makes various network statistics available, such as the total number of blocks in existence, next difficulty retarget block, total BTC mined in the past 24 hours etc.

Example usage:

```csharp
using System;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Statistics;

namespace TestProj
{
    class Program
    {
        private static StatisticsExplorer _explorer;

        static void Main(string[] args)
        {
            try
            {
                _explorer = new StatisticsExplorer();
                var stats = _explorer.GetAsync().Result;

                Console.WriteLine("The current difficulty is {0}. The next retarget will happen in {1} hours",
                    stats.Difficulty,
                    (int)((stats.NextRetarget - stats.TotalBlocks) * stats.MinutesBetweenBlocks / 60));
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}
```
