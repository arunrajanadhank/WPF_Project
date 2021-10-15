using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using CryptoCurrencyDataCollector.Common;
using CryptoCurrencyDataCollector.Contracts;

namespace CryptoCurrencyDataCollector
{
    public class JsonParser : IDataParser
    {
        public CryptoDataModel ParseData(string jsonData)
        {
            CryptoDataModel dataModel = null;

            try
            {
                LoggerUtil.LogInfo("Parsing the JSON data.");

                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    dataModel = JsonConvert.DeserializeObject<CryptoDataModel>(jsonData);

                    LoggerUtil.LogInfo("Successfully parsed the JSON data to CryptoDataModel.");
                }
            }
            catch (Exception ex)
            {
                LoggerUtil.LogException(ex);
            }

            return dataModel;
        }
    }
}
