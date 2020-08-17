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

            this.Loaded += SetWinStatusActive;
            this.Closing += SetWinStatusInactive;

            mainWin = win;
        }

        private void GetTypeOfAccount(object sender, RoutedEventArgs e)
        {
            decimal sum = 0;
            try
            {
                sum = Convert.ToDecimal(textBoxSumAccount.Text);
            }
            catch (Exception)
            {
                this.Close();
                MessageBox.Show("Введен некорректный формат.");
                return;
            }
            this.Close();
            if (radioButtonDemand.IsChecked == true)
                mainWin.OpenAccount(AccountType.Ordinary, sum);
            else
                mainWin.OpenAccount(AccountType.Deposit, sum);
        }

        private void SetWinStatusActive(object sender, RoutedEventArgs e)
        {
            mainWin.OtherActiveWin = this;
        }

        private void SetWinStatusInactive(object sender, System.ComponentModel.CancelEventArgs e)
        {
            mainWin.OtherActiveWin = null;
        }
    }
}
