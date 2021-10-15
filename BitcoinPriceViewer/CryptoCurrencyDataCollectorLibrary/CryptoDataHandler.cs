using CryptoCurrencyDataCollector.Common;
using CryptoCurrencyDataCollector.Contracts;

namespace CryptoCurrencyDataCollector
{
    public class CryptoDataHandler : ICryptoDataReader
    {
        IDataSource _dataSource;
        IDataParser _dataParser;

        public CryptoDataHandler(IDataSource dataSource, IDataParser dataParser)
        {
            _dataSource = dataSource;
            _dataParser = dataParser;
        }

        public CryptoDataModel GetClosingPrices()
        {
            string jsonData = string.Empty;
            CryptoDataModel dataModel = null;

            LoggerUtil.LogInfo("Get Closing Prices.");
            jsonData = _dataSource.ReadDataFromSource(Constants.requestUri);

            if (!string.IsNullOrEmpty(jsonData))
            {
                dataModel = _dataParser.ParseData(jsonData);
            }
            else
            {
                LoggerUtil.LogError("Unable to read data from source.");
            }

            return dataModel;
        }
    }
}
