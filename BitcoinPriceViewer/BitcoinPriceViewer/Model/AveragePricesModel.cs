using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencyDataCollector;

namespace BitcoinPriceViewer.Model
{
    internal class AveragePricesModel
    {
        public static float CalculateAveragePrices(DateTime startDateTime, DateTime endDateTime, CryptoDataModel dataModel)
        {
            float sumCoinValue = 0;
            int count = 0;
            float avgCoinValue = 0;

            try
            {
                while (startDateTime <= endDateTime)
                {
                    if (!dataModel.Bpi.TryGetValue(startDateTime.ToString("yyyy-MM-dd"), out float fValue))
                    {
                        LoggerUtil.LogInfo("Specified date is not present in the dataModel.");
                        break;
                    }

                    sumCoinValue += fValue;
                    count++;
                    startDateTime = startDateTime.AddDays(1);
                }

                avgCoinValue = sumCoinValue / count;
            }
            catch (Exception ex)
            {
                LoggerUtil.LogException(ex);
            }

            return avgCoinValue;
        }
    }
}
