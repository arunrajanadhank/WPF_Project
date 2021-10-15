using System;
using System.Collections.Generic;

namespace CryptoCurrencyDataCollector
{
    public class CryptoDataModel
    {
        public Dictionary<string, float> Bpi { get; set; }
        public string Disclaimer { get; set; }
        public Time Time { get; set; }
    }

    public class Time
    {
        public string Updated { get; set; }
        public DateTime UpdatedISO { get; set; }
    }
}
