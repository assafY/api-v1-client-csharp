## `Info.Blockchain.Api.BlockExplorer` namespace

The `BlockExplorer` namespace contains the `BlockExplorer` class that reflects the functionality documented at  https://blockchain.info/api/blockchain_api. It can be used to query the block chain, fetch block, transaction and address data, get unspent outputs for an address etc.

Example usage:

```csharp
using System;
using System.Linq;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.BlockExplorer;
using Info.Blockchain.Api.Data;

namespace TestApp
{
    class Program
    {
        private static BlockExplorer _explorer;

        static void Main(string[] args)
        {
            try
            {
                // instantiate a block explorer with no api code
                _explorer = new BlockExplorer();

                // get a transaction by hash and list the value of all its inputs
                var tx = _explorer.GetTransactionAsync("df67414652722d38b43dcbcac6927c97626a65bd4e76a2e2787e22948a7c5c47").Result;
                foreach (Input i in tx.Inputs)
                {
                    Console.WriteLine(i.PreviousOutput.Value);
                }

                // get a block by hash and read the number of transactions in the block
                var block = _explorer.GetBlockAsync("0000000000000000050fe18c9b961fc7c275f02630309226b15625276c714bf1").Result;
                int numberOfTxsInBlock = block.Transactions.Count;

                // get an address by hash...
                var address = _explorer.GetAddressAsync("1e15be27e4763513af36364674eebdba5a047323").Result;

                // or by address string...
                address = _explorer.GetAddressAsync("13k5KUK2vswXRdjgjxgCorGoY2EFGMFTnu").Result;
                
                // and print its final balance
                var finalBalance = address.FinalBalance;

                // get a list of currently unconfirmed transactions...
                var unconfirmedTxs = _explorer.GetUnconfirmedTransactionsAsync().Result;

                // and get the relay IP address for each
                var relayIPs = unconfirmedTxs.Select(v => v.RelayedBy);

                // calculate the balanace of an address by fetching a list of all its unspent outputs
                var outs = _explorer.GetUnspentOutputsAsync("13k5KUK2vswXRdjgjxgCorGoY2EFGMFTnu").Result;
                var totalUnspentValue = outs.Sum(v => v.Value.GetBtc());

                // get the latest block on the main chain and read its height
                var latestBlock = _explorer.GetLatestBlockAsync().Result;
                long latestBlockHeight = latestBlock.Height;

                // use the previous block height to get a list of blocks at that height
                // and detect a potential chain fork
                var blocksAtHeight = _explorer.GetBlocksAtHeightAsync(latestBlockHeight).Result;
                if (blocksAtHeight.Count > 1)
                    Console.WriteLine("The chain has forked!");
                else
                    Console.WriteLine("The chain is still in one piece :)");
                
                // get a list of all blocks that were mined today since 00:00 UTC
                var todaysBlocks = _explorer.GetBlocksAsync().Result;

                // get a list of all blocks that were mined yesterday using DateTime
                var yesterdaysBlocks = _explorer.GetBlocksAsync(DateTime.Now.AddDays(-1)).Result;

                // get a list of all blocks mined on a particular day using a unix timestamp...
                var someDaysBlocks = _explorer.GetBlocksAsync(1490000210396).Result;

                // or a unix timestamp string
                someDaysBlocks = _explorer.GetBlocksAsync("1490000210396").Result;
                
                // you can also get a particular mining pool's recent blocks
                // note: this approach is case-sensitive
                var minePoolBlocks = _explorer.GetBlocksAsync("BTC.com").Result;
            }
            catch (ClientApiException e)
            {
                Console.WriteLine("Blockchain exception: " + e.Message);
            }
        }
    }
}

```
