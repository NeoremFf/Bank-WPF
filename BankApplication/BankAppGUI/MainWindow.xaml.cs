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
        public DateTime date;
        private static Bank<Account> bank;
        public int currentAccountId = -1;

        public MainWindow()
        {
            InitializeComponent();

            bank = new Bank<Account>("GoldBank");
            date = DateTime.Now;
            bank._date = date;

            textBlockBankName.Text = bank.Name;
            UpdateDate();

            UpdateAccountInfoUI();
        }

        public void UpdateDate()
        {
            textBlock_CurrentDate.Text = date.ToString("d");
            bank._date = date;

            // check persents status
            foreach (var item in bank)
            {
                DepositAccount dep = item as DepositAccount;
                if (dep != null) dep.Calculate(date);
            }

            if (currentAccountId != -1)
                UpdateAccountInfoUI(bank.FindAccount(currentAccountId));
        }

        public void OpenAccount(AccountType type, decimal sum)
        {
            bank.Open(type,
                sum,
                ShowInfo,
                ShowInfo,
                ShowInfo,
                ShowInfo);
        }

        public void TransactionOnAccount_Put(decimal sum)
        {
            if (currentAccountId == -1) throw new Exception("Error of account id");

            Account acc = bank.FindAccount(currentAccountId);
            acc.Put(sum);
            UpdateAccountInfoUI(acc);
        }

        public void TransactionOnAccount_Withdraw(decimal sum)
        {
            if (currentAccountId == -1) throw new Exception("Error of account id");

            Account acc = bank.FindAccount(currentAccountId);
            acc.Withdraw(sum);
            UpdateAccountInfoUI(acc);
        }

        public void GetAccountInfo(int id)
        {
            Account acc = bank.FindAccount(id);
            if (acc == null)
            {
                MessageBox.Show("Счета с данным id нет.");
                return;
            }
            currentAccountId = id;
            UpdateAccountInfoUI(acc);
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

        private void AccountMenu_Put(object sender, RoutedEventArgs e)
        {
            MoneyTransactionWin transactionWin = new MoneyTransactionWin(this, TransactionType.Put);
            transactionWin.Show();
        }

        private void AccountMenu_Withdraw(object sender, RoutedEventArgs e)
        {
            MoneyTransactionWin transactionWin = new MoneyTransactionWin(this, TransactionType.Withdraw);
            transactionWin.Show();
        }

        private void AccountMenu_CloseAccount(object sender, RoutedEventArgs e)
        {
            if (currentAccountId == -1) throw new Exception("Error of account id");

            bank.Close(currentAccountId);
            currentAccountId = -1;
            UpdateAccountInfoUI();
        }

        private void SetDAte(object sender, RoutedEventArgs e)
        {
            SetDateWin dateWin = new SetDateWin(this, date);
            dateWin.Show();
        }

        private static void ShowInfo(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void UpdateAccountInfoUI(Account acc = null)
        {
            if (currentAccountId == -1 || acc == null)
            {
                string emptyAcc = "***";
                textBlock_name.Text = emptyAcc;
                textBlock_type.Text = emptyAcc;
                textBlock_value.Text = emptyAcc;
                textBlock_daysLeft.Text = emptyAcc;

                buttonsManagersAccount.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlock_name.Text = $"Счет #{acc.Id}";
                
                if (acc is DepositAccount depAcc)
                {
                    textBlock_type.Text = "Депозит";
                    textBlock_value.Text = $"{acc.Sum:#.##} ( +{depAcc.SumPersent:#.##} )";

                    textBlock_daysLeft_Title.Visibility = Visibility.Visible;
                    textBlock_daysLeft.Visibility = Visibility.Visible;
                    textBlock_daysLeft.Text = depAcc.GetDaysLeftToCalculate(date).ToString();
                }
                else
                {
                    textBlock_type.Text = "Расчетный";
                    textBlock_value.Text = acc.Sum.ToString();

                    textBlock_daysLeft_Title.Visibility = Visibility.Hidden;
                    textBlock_daysLeft.Visibility = Visibility.Hidden;
                }

                buttonsManagersAccount.Visibility = Visibility.Visible;
            }
        }
    }
}
