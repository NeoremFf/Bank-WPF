using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public abstract class Account : IAccount
    {
        // -------------------------------------
        // events
        // -------------------------------------
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;

        protected internal event AccountdHandler OpenedNewAccEvent;
        protected internal event AccountdHandler CloseAccEvent;

        static public int Counter = 0; // for set new id

        public Account(decimal sum, float percentage, AccountType _type, int newId = 0)
        {
            if (newId != 0) Counter = newId; ;
            Id = Counter++;
            Sum = sum;
            Percentage = percentage;
            type = _type;
        }

        public int Id { get; private set; }
        // Current sum
        public decimal Sum { get; private set; }
        // Percentage for deposit account
        public float Percentage { get; private set; }

        // type of account
        public readonly AccountType type;

        // -------------------------------------
        // events manager
        // -------------------------------------
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
            OpenedNewAccEvent?.Invoke(this);
        }
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
            CloseAccEvent?.Invoke(this);
        }

        // -------------------------------------
        // Account manager
        // -------------------------------------

        // Put maney to account
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("На счет поступило " + sum, sum));
        }

        // Withdraw from account, returns sum that was withdrawn from account
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }

        // Open account event
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет! Id счета: {Id}", Sum));
        }

        // Close account event
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счет {Id} закрыт.  Итоговая сумма: {Sum}", Sum));
        }

        // Calculate percentage to sum
        public virtual decimal Calculate(DateTime currentDate)
        {
            decimal increment = Sum * (decimal)Percentage / 100;
            Sum += increment;
            return increment;
        }
    }
}
