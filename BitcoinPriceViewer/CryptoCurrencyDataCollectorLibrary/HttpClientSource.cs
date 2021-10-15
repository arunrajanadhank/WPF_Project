using CryptoCurrencyDataCollector.Common;
using CryptoCurrencyDataCollector.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CryptoCurrencyDataCollector
{
    public class HttpClientSource : IDataSource
    {
        public string ReadDataFromSource(string request)
        {
            HttpClient httpClient = null;
            string data = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(request))
                {
                    LoggerUtil.LogInfo($"Reading data from source. Source={request}");

                    httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.MediaType));

                    Task.Run(async () =>
                    {
                        HttpResponseMessage httpResponse = new HttpResponseMessage();
                        httpResponse = await httpClient.GetAsync(request);

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            data = await httpResponse.Content.ReadAsStringAsync();
                            LoggerUtil.LogInfo("Reading data success.");
                        }
                    }).Wait();
                }
                else
                {
                    LoggerUtil.LogError("Source Uri is empty.");
                }
            }
            catch (Exception ex)
            {
                LoggerUtil.LogException(ex);
            }
            finally
            {
                httpClient?.Dispose();
                httpClient = null;
            }

            return data;
        }
    }
}
