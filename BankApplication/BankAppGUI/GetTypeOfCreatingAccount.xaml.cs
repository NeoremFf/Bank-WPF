using BankLib;
using System;
using System.Diagnostics;
using System.Windows;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for GetTypeOfCreatingAccount.xaml
    /// Create account | Get type of account and start sum
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

        // Get summ from window text box
        // then create an account with type that choose from window radio button
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

        // -----------------------------------------------------------------------
        // Update state of current active window
        // -----------------------------------------------------------------------
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
