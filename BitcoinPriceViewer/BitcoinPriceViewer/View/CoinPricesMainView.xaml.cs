using BitcoinPriceViewer.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BitcoinPriceViewer.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CoinPricesMainView : Window
    {
        private CoinPricesMainViewModel coinPricesViewModel;
        public CoinPricesMainView()
        {
            InitializeComponent();

            coinPricesViewModel = new CoinPricesMainViewModel();
            DataContext = coinPricesViewModel;
        }

        private void MainGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
           coinPricesViewModel.GetAverageCoinPrices();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            coinPricesViewModel.GetDataFromSource();
            coinPricesViewModel.ViewUpdateStatus = "[View Updated On: " + DateTime.Now.ToString() + "]";
        }
    }
}
