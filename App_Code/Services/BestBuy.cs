using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;


public class BestBuy
{
	readonly static string _serviceUrl = WebConfigurationManager.AppSettings["best-buy-url"];
	readonly static string _apiKey = WebConfigurationManager.AppSettings["best-buy-api-key"];

	public string ApiKey { get; private set; }

	private BestBuy(string apiKey)
	{
		ApiKey = apiKey;
	}

	public static BestBuy Auth()
	{
		/*here we can handle the key
		  if it will change in the future*/
		return new BestBuy(_apiKey);
	}

	
	public static async Task<T> PerformRequest<T>(string action)
	{
		try
		{
			ServicePointManager.SecurityProtocol =
			SecurityProtocolType.Tls12 |
			SecurityProtocolType.Tls11 |
			SecurityProtocolType.Tls;

			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(_serviceUrl + action);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					return default(T);
				}
				else
				{
					var answer = await response.Content.ReadAsAsync<T>();
					return answer;

				}
			}
		}
		catch(Exception)
		{
			//TODO: Log here
			return default(T);
		}
	}

	public Task<BestBuyProductResponse> GetProducts(int pageSize = 10, int page = 1)
	{
		string queryString = "pageSize=" + pageSize + "&page=" + page + "&format=json&show=sku,name,salePrice,image&apiKey=" + this.ApiKey;
		return PerformRequest<BestBuyProductResponse>("products?" + queryString);
	}

	public Task<Product> GetProduct(int sku)
	{
		string queryString = "apiKey=" + this.ApiKey;
		return PerformRequest<Product>("products/" + sku + ".json?" + queryString);
	}
}