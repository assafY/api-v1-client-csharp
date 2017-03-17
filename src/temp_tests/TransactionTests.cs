using System.Collections.ObjectModel;
using Info.Blockchain.Api.Client;
using Info.Blockchain.Api.Data;
using Info.Blockchain.Api.Tests;
using KellermanSoftware.CompareNetObjects;
using Xunit;

namespace Info.Blockchain.API.Tests.IntegrationTests
{
	public class TransactionTests
	{
		[Fact]
		public async void GetTransaction_ByHash_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				Transaction knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
				Transaction receivedTransaction = await apiHelper._blockExplorer.GetTransactionAsync(knownTransaction.Hash);

				CompareLogic compareLogic = new CompareLogic();
				ComparisonResult comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
				Assert.True(comparisonResult.AreEqual);
			}
		}

		[Fact]
		public async void GetTransaction_ByIndex_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				Transaction knownTransaction = ReflectionUtil.DeserializeFile<Transaction>("single_transaction");
				Transaction receivedTransaction = await apiHelper._blockExplorer.GetTransactionByIndexAsync(knownTransaction.Index);

				CompareLogic compareLogic = new CompareLogic();
				ComparisonResult comparisonResult = compareLogic.Compare(knownTransaction, receivedTransaction);
				Assert.True(comparisonResult.AreEqual);
			}
		}

		[Fact]
		public async void GetUnconfirmedTransaction_Valid()
		{
			using (ApiHelper apiHelper = new ApiHelper())
			{
				ReadOnlyCollection<Transaction> unconfirmedTransactions = await apiHelper._blockExplorer.GetUnconfirmedTransactionsAsync();
				
				Assert.NotNull(unconfirmedTransactions);
			}
		}
	}
}
