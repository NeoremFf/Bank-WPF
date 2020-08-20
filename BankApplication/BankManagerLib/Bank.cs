using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    // type of account
    public enum AccountType
    {
        Ordinary,
        Deposit
    }

    public class Bank<T> : IEnumerable
        where T : Account
    {
        private T[] accounts; // all accouns in bank

        public const float DEPOSIT_PERCENTAGE = 12;

        public DateTime _date { get; set; }
        public string Name { get; private set; }

        // main handlers for accounts
        private AccountStateHandler Added;
        private AccountStateHandler Withdrawed;
        private AccountStateHandler Closed;
        private AccountStateHandler Opened;
        private AccountdHandler OpenedNewAccEvent;
        private AccountdHandler CloseAccEvent;
        private AccountdHandler UpdateValueAccEvent;

        public Bank(string name, AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            this.Name = name;
            Added += addSumHandler;
            Withdrawed += withdrawSumHandler;
            Closed += closeAccountHandler;
            Opened += openAccountHandler;
        }

        public void SetHandlers(AccountdHandler open_handler, AccountdHandler delete_handler, AccountdHandler update_handler)
        {
            OpenedNewAccEvent += open_handler;
            CloseAccEvent += delete_handler;
            UpdateValueAccEvent += update_handler;
        }

        // -----------------------------------------
        // create account
        // -----------------------------------------
        
        // with type and start sum
        public void Open(AccountType accountType, decimal sum)
        {
            T newAccount = null;

            switch (accountType)
            {
                // Тип аккаунта
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 0, AccountType.Ordinary) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, DEPOSIT_PERCENTAGE, AccountType.Deposit, _date) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Ошибка создания счета");
            // добавляем новый счет в массив счетов      
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Added += Added;
            newAccount.Withdrawed += Withdrawed;
            newAccount.Closed += Closed;
            newAccount.Opened += Opened;
            newAccount.OpenedNewAccEvent += OpenedNewAccEvent;
            newAccount.CloseAccEvent += CloseAccEvent;
            newAccount.UpdateValueAccEvent += UpdateValueAccEvent;

            newAccount.Open();
        }

        // with existing  account
        public void Open(T newAccount)
        {
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Added += Added;
            newAccount.Withdrawed += Withdrawed;
            newAccount.Closed += Closed;
            newAccount.Opened += Opened;
            newAccount.OpenedNewAccEvent += OpenedNewAccEvent;
            newAccount.CloseAccEvent += CloseAccEvent;
            newAccount.UpdateValueAccEvent += UpdateValueAccEvent;
        }

        // add sum to account
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Put(sum);
        }

        // Withdraw sum from account
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Withdraw(sum);
        }

        // close account
        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Счет не найден");

            account.Close();

            if (accounts.Length <= 1)
                accounts = null;
            else
            {
                // уменьшаем массив счетов, удаляя из него закрытый счет
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                {
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                }
                accounts = tempAccounts;
            }
        }

        // find account by id
        public T FindAccount(int id)
        {
            if (accounts == null)
                return null;
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }
        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }

        public bool HasAccounts()
        {
            if (accounts == null || accounts.Length == 0)
                return false;
            else
                return true;
        }

        public override string ToString()
        {
            string str = string.Empty;
            for (int i = 0; i < accounts.Length; i++)
            {
                str += $"Аккаунт {accounts[i].Id}:\n - Тип: {accounts[i].type.ToString()}\n - Баланс: {accounts[i].Sum}\n";
            }
            return str;
        }

        public IEnumerator GetEnumerator()
        {
            if (accounts == null)
                yield break;
            for (int i = 0; i < accounts.Length; i++)
            {
                if (accounts[i].type == AccountType.Deposit)
                    yield return accounts[i];
            }
        }
    }
}
