using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyDataCollector.Common
{
    internal static class Constants
    {
        public static readonly string Bpi = "bpi";
        public static readonly string MediaType = "application/json";

        public static readonly string requestUri = "https://api.coindesk.com/v1/bpi/historical/close.json";
    }
}
