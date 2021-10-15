using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BitcoinPriceViewer.View;
using BitcoinPriceViewer.ViewModel;

namespace BitcoinPriceViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Initialize main window
            CoinPricesMainView coinPricesMainView = new CoinPricesMainView();
            coinPricesMainView.Show();
        }
    }
}
