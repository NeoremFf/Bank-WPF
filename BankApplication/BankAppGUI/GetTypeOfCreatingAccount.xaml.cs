using BankLib;
using System;
using System.Diagnostics;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for GetTypeOfCreatingAccount.xaml
    /// </summary>
    public partial class GetTypeOfCreatingAccount : Window
    {
        MainWindow mainWin;

        public GetTypeOfCreatingAccount(MainWindow win)
        {
            InitializeComponent();

            mainWin = win;
        }

        private void GetTypeOfAccount(object sender, RoutedEventArgs e)
        {
            decimal sum = Convert.ToDecimal(textBoxSumAccount.Text);
            if (radioButtonDemand.IsChecked == true)
                mainWin.OpenAccount(AccountType.Ordinary, sum);
            else
                mainWin.OpenAccount(AccountType.Deposit, sum);

            this.Close();
        }
    }
}
