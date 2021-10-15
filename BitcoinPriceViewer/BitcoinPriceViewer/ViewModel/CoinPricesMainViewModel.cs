using BitcoinPriceViewer.Common;
using BitcoinPriceViewer.Model;
using CryptoCurrencyDataCollector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BitcoinPriceViewer.ViewModel
{
    public class CoinPricesMainViewModel : INotifyPropertyChanged
    {
        private CryptoDataHandler dataHandler;
        private CryptoDataModel dataModel;
        private DateTime? startDate = DateTime.Now;
        private DateTime? endDate = DateTime.Now;
        private string message;
        private string averagePrice;
        private string viewUpdateMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, float> CoinValues { get; set; }

        public string StatusUpdate { get; set; }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                if (value != message)
                {
                    message = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ViewUpdateStatus
        {
            get
            {
                return viewUpdateMessage;
            }
            set
            {
                if (value != viewUpdateMessage)
                {
                    viewUpdateMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AveragePrice
        {
            get
            {
                return averagePrice;
            }
            set
            {
                if (value != averagePrice)
                {
                    averagePrice = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime? SelectedStartDate
        {
            get { return startDate; }

            set
            {
                if (value > SelectedEndDate)
                {
                    Message = Constants.StartDateErrorMessage;
                }
                else
                {
                    Message = string.Empty;
                }
                startDate = value;
                OnPropertyChanged("StartDatePicker");
            }
        }

        public DateTime? SelectedEndDate
        {
            get { return endDate; }

            set
            {
                if (value < SelectedStartDate)
                {
                    Message = Constants.StartDateErrorMessage;
                }
                else
                {
                    Message = string.Empty;
                }
                endDate = value;
                OnPropertyChanged("EndDatePicker");
            }
        }

        public CoinPricesMainViewModel()
        {
            dataHandler = new CryptoDataHandler(new HttpClientSource(), new JsonParser());
            GetDataFromSource();
        }

        public void GetDataFromSource()
        {
            dataModel = dataHandler.GetClosingPrices();
            CoinValues = dataModel.Bpi;
            StatusUpdate = dataModel.Time.UpdatedISO.ToString();
        }

        public void GetAverageCoinPrices()
        {
            var averagePrice = AveragePricesModel.CalculateAveragePrices(SelectedStartDate.Value, SelectedEndDate.Value, dataModel);
            AveragePrice = averagePrice.ToString();
        }

        private void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
    }
}
