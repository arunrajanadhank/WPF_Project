﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencyDataCollector.Contracts
{
    public interface ICryptoDataReader
    {
        CryptoDataModel GetClosingPrices();
    }
}
