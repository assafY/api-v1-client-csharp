using System;
using System.Threading.Tasks;
using Info.Blockchain.API.Client;
using Info.Blockchain.API.Models;

namespace Info.Blockchain.API.Statistics
{
	/// <summary>
	/// This class allows users to get the bitcoin network statistics.
	/// It reflects the functionality documented at https://blockchain.info/api/charts_api
	/// </summary>
	public class StatisticsExplorer
	{
		private readonly IHttpClient httpClient;
		public StatisticsExplorer()
		{
			httpClient = new BlockchainHttpClient(uri: "https://api.blockchain.info/");
		}
		internal StatisticsExplorer(IHttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		/// <summary>
		/// Gets the network statistics.
		/// </summary>
		/// <returns>An instance of the StatisticsResponse class</returns>
		/// <exception cref="ServerApiException">If the server returns an error</exception>
		public async Task<StatisticsResponse> GetStatsAsync()
		{
			return await httpClient.GetAsync<StatisticsResponse>("stats");
		}

        /// <summary>
        /// Gets chart data for a specified chart
        /// </summary>
        /// <param name="chartType">Chart name</param>
        /// <returns>Chart data?</returns> <summary>
        /// <param name="timespan">Optional timespan to include</param>
        /// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<dynamic> GetChartAsync(string chartType, string timespan = null)
        {
            var queryString = new QueryString();

            try
            {

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No chart with this name"))
                {
                    throw new ArgumentOutOfRangeException(nameof(chartType), "This chart name does not exist");
                }
                throw;
            }
        }
	}
}
