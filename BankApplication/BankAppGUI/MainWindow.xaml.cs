using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BankLib;

namespace BankAppGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Main windon
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime date; // current date
        public int currentAccountId = -1; // id of the current account in the bank about which information was received
        private static Bank<Account> bank;

        public Window OtherActiveWin { get; set; } = null; // ref to current opened window

        public MainWindow()
        {
            InitializeComponent();

            bank = new Bank<Account>("GoldBank", 
                ShowInfo,
                ShowInfo,
                ShowInfo,
                ShowInfo);
            bank.SetHandlers(SaveToDatabase, RemoveFromDatabase);
            date = DateTime.Now;
            bank._date = date;

            // Update data from database if it is
            var accountsData = ReadFromDatabase();
            if (accountsData.Count != 0)
                foreach (var item in accountsData)
                    bank.Open(item);

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
            bank.Open(type, sum);
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

        // -----------------------------------------------------------------------
        // Functions that call in main window
        // -----------------------------------------------------------------------
        private void MenuButton_OpenAccount(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            GetTypeOfCreatingAccount getTypeWin = new GetTypeOfCreatingAccount(this);
            getTypeWin.Owner = this;
            getTypeWin.Show();
        }

        private void MenuButton_FindAccountAtId(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            FindAccountAtId findWin = new FindAccountAtId(this);
            findWin.Owner = this;
            findWin.Show();
        }

        private void AccountMenu_Put(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            MoneyTransactionWin transactionWin = new MoneyTransactionWin(this, TransactionType.Put);
            transactionWin.Owner = this;
            transactionWin.Show();
        }

        private void AccountMenu_Withdraw(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            MoneyTransactionWin transactionWin = new MoneyTransactionWin(this, TransactionType.Withdraw);
            transactionWin.Owner = this;
            transactionWin.Show();
        }

        private void AccountMenu_CloseAccount(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            if (currentAccountId == -1) throw new Exception("Error of account id");

            bank.Close(currentAccountId);
            currentAccountId = -1;
            UpdateAccountInfoUI();
        }

        private void SetDAte(object sender, RoutedEventArgs e)
        {
            if (OtherActiveWin != null)
                return;

            SetDateWin dateWin = new SetDateWin(this, date);
            dateWin.Owner = this;
            dateWin.Show();
        }


        // -----------------------------------------------------------------------
        // Show info about current account on window GUI
        // -----------------------------------------------------------------------
        private static void ShowInfo(object sender, AccountEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        // -----------------------------------------------------------------------
        // Update info about current account
        // -----------------------------------------------------------------------
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


        // -----------------------------------------------------------------------
        // Database
        // -----------------------------------------------------------------------
        private void SaveToDatabase(Account acc)
        {
            using (MyDbContext db = new MyDbContext())
            {
                DateTime? _date = null;
                if (acc.type == AccountType.Deposit)
                {
                    DepositAccount _dep = acc as DepositAccount;
                    _date = _dep.dateOpen;
                }
                var dataAcc = new AccountData() { AccountId = acc.Id, Type = acc .type.ToString(), Sum = acc.Sum, Date = _date };
                db.AccountsData.Add(dataAcc);
                db.SaveChanges();
            }
        }
        private void RemoveFromDatabase(Account acc)
        {
            using (MyDbContext db = new MyDbContext())
            {
                var item = db.AccountsData.Where(t => t.AccountId == acc.Id).First();
                db.AccountsData.Remove(item);
                db.SaveChanges();
            }
        }
        private List<Account> ReadFromDatabase()
        {
            List<Account> accounts = new List<Account>();
            using (MyDbContext db = new MyDbContext())
            {
                foreach (var item in db.AccountsData)
                {
                    Account acc;
                    if (item.Type == "Deposit")
                    {
                        int id = item.AccountId;
                        float pers = Bank<Account>.DEPOSIT_PERCENTAGE;
                        AccountType type = AccountType.Deposit;
                        acc = new DepositAccount(item.Sum, pers, type, (DateTime)item.Date, id) as Account;
                    }
                    else
                    {
                        int id = item.AccountId;
                        float pers = 0;
                        AccountType type = AccountType.Ordinary;
                        acc = new DemandAccount(item.Sum, pers, type, id) as Account;
                    }
                    if (acc != null) accounts.Add(acc);
                }
            }
            return accounts;
        }
    }
}
