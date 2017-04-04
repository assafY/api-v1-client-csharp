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
			httpClient = new BlockchainHttpClient("https://api.blockchain.info");
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
            var queryString = new QueryString();
            queryString.Add("format","json");
			return await httpClient.GetAsync<StatisticsResponse>("stats", queryString);
		}

        /// <summary>
        /// Gets chart data for a specified chart
        /// </summary>
        /// <param name="chartType">Chart name</param>
        /// <param name="timespan">Optional timespan to include</param>
        /// <returns>Chart response object</returns>
        /// <exception cref="ServerApiException">If the server returns an error</exception>
        public async Task<ChartResponse> GetChartAsync(string chartType, string timespan = null)
        {
            var queryString = new QueryString();
            queryString.Add("format","json");
            if (timespan != null)
            {
                queryString.Add("timespan", timespan);
            }
            try
            {
                return await httpClient.GetAsync<ChartResponse>("charts/" + chartType, queryString);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No chart with this name"))
                {
                    throw new ArgumentOutOfRangeException(nameof(chartType), "This chart name does not exist");
                }
                if (ex.Message.Contains("Could not parse timestring"))
                {
                    throw new ArgumentOutOfRangeException(nameof(timespan), "Incorrect timespan format");
                }
                throw;
            }
        }
	}
}
