using System;
using System.Windows;

namespace BankAppGUI
{
    public enum TransactionType
    {
        Put,
        Withdraw
    }

    /// <summary>
    /// Interaction logic for MoneyTransactionWin.xaml
    /// Получение суммы для транзакции, тип транзакции
    /// </summary>
    public partial class MoneyTransactionWin : Window
    {
        MainWindow mainWin;
        TransactionType type;

        public MoneyTransactionWin(MainWindow _mainWin, TransactionType _type)
        {
            InitializeComponent();

            this.Loaded += SetWinStatusActive;
            this.Closing += SetWinStatusInactive;

            mainWin = _mainWin;
            type = _type;

            switch (type)
            {
                case TransactionType.Put:
                    button_Accept.Content = "Пополнить";
                    break;
                case TransactionType.Withdraw:
                    button_Accept.Content = "Снять";
                    break;
            }
        }

        private void Button_Transaction(object sender, RoutedEventArgs e)
        {
            decimal sum = 0;
            try
            {
                sum = Convert.ToDecimal(textBox_Sum.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Введен некорректный формат.");
            }
            finally
            {
                switch (type)
                {
                    case TransactionType.Put:
                        mainWin.TransactionOnAccount_Put(sum);
                        break;
                    case TransactionType.Withdraw:
                        mainWin.TransactionOnAccount_Withdraw(sum);
                        break;
                }
                this.Close();
            }
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
