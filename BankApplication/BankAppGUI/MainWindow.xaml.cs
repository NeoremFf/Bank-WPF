using System;
using System.Windows;
using BankLib;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Bank<Account> bank;
        DateTime date;

        public MainWindow()
        {
            InitializeComponent();

            bank = new Bank<Account>("GoldBank");
            date = DateTime.Now;

            textBlockBankName.Text = bank.Name;
            CurrentDateUI.Text = date.ToString("d");

            UpdateAccountInfoUI();
        }

        public void OpenAccount(AccountType type, decimal sum)
        {
            bank.Open(type,
                sum,
                ShowInfo,
                ShowInfo,
                ShowInfo,
                ShowInfo,
                ShowInfo);
        }

        public void GetAccountInfo(int id)
        {
            Account acc = bank.FindAccount(id);
            if (acc == null)
            {
                MessageBox.Show("Счета с данным id нет.");
                return;
            }
            UpdateAccountInfoUI(acc.Id.ToString(), acc.type.ToString(), acc.Sum.ToString(), "0");
        }

        private void MenuButton_OpenAccount(object sender, RoutedEventArgs e)
        {
            GetTypeOfCreatingAccount getTypeWin = new GetTypeOfCreatingAccount(this);
            getTypeWin.Show();
        }

        private void MenuButton_FindAccountAtId(object sender, RoutedEventArgs e)
        {
            FindAccountAtId findWin = new FindAccountAtId(this);
            findWin.Show();
        }

        private static void ShowInfo(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void UpdateAccountInfoUI(string name = "***", string type = "***", string value = "***", string days = "***")
        {
            textBlock_name.Text = $"Счет #{name}";
            textBlock_type.Text = type;
            textBlock_value.Text = value;
            textBlock_daysLeft.Text = days;

            if (textBlock_name.Text == "***")
                buttonsManagersAccount.Visibility = Visibility.Hidden;
            else
                buttonsManagersAccount.Visibility = Visibility.Visible;
        }
    }
}
