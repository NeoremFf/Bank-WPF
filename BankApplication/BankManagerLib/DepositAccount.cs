using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    /// <summary>
    /// Account with percentage
    /// </summary>
    public class DepositAccount : Account
    {
        public DateTime dateOpen { get; private set; } // the date of 30-days period starts
        public DateTime dateToCalculate { get; private set; } // the date of 30-days period ends
        public decimal SumPersent = 0.0m; // money that was getting from percentage

        public DepositAccount(decimal sum, float percentage, AccountType _type, DateTime _date, int id = 0) : base(sum, percentage, _type, id)
        {
            dateOpen = _date;
            dateToCalculate = dateOpen.AddDays(30);
        }

        // count of days that left to calculate percentage
        public int GetDaysLeftToCalculate(DateTime currentDate) => (int)(dateToCalculate - currentDate).TotalDays;

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый депозитный счет! Id счета: {this.Id}", this.Sum));
        }

        public override void Put(decimal sum)
        {
            base.Put(sum);
            SumPersent = 0.0m;
            // if it was a real bank -> date was correctly
            dateOpen = DateTime.Now; 
            dateToCalculate = dateOpen.AddDays(30);
        }

        public override decimal Withdraw(decimal sum)
        {
            decimal sumWithdraw = base.Withdraw(sum);
            SumPersent = 0.0m;
            // if it was a real bank -> date was correctly
            dateOpen = DateTime.Now; 
            dateToCalculate = dateOpen.AddDays(30);
            return sumWithdraw;
        }

        // Calculate percentage
        public override decimal Calculate(DateTime currentDate)
        { 
            if (currentDate >= dateToCalculate)
            {
                SumPersent += base.Calculate(currentDate);
                dateOpen = currentDate;
                dateToCalculate = dateOpen.AddDays(30);
            }
            return 0;
        }
    }
}
