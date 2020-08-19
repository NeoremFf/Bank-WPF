﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    // тип счета
    public enum AccountType
    {
        Ordinary,
        Deposit
    }

    public class Bank<T> : IEnumerable
        where T : Account
    {
        private T[] accounts;

        public DateTime _date { get; set; }
        public string Name { get; private set; }

        public Bank(string name)
        {
            this.Name = name;
        }

        // метод создания счета
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler,
            AccountStateHandler closeAccountHandler,
            AccountStateHandler openAccountHandler)
        {
            T newAccount = null;

            switch (accountType)
            {
                // Тип аккаунта
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 0, AccountType.Ordinary) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 12, AccountType.Deposit, _date) as T;
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
            // установка обработчиков событий счета
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;

            newAccount.Open();
        }

        //добавление средств на счет
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Put(sum);
        }

        // вывод средств
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Счет не найден");
            account.Withdraw(sum);
        }

        // закрытие счета
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

        // поиск счета по id
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
        // перегруженная версия поиска счета
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