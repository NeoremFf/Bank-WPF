using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public class DepositAccount : Account
    {
        public DateTime dateOpen { get; private set; } // дата начала 30-ти дневного периода
        public DateTime dateToCalculate { get; private set; } // дата конца 30-ти дневного периода

        public decimal SumPersent = 0.0m; // сумма денег, которые были начисленны процентами за всё время существования счета

        public DepositAccount(decimal sum, float percentage, AccountType _type, DateTime _date) : base(sum, percentage, _type)
        {
            dateOpen = _date;
            dateToCalculate = dateOpen.AddDays(30);
        }

        // к-во оставшихся дней до начисления процентов
        public int GetDaysLeftToCalculate(DateTime currentDate) => (int)(dateToCalculate - currentDate).TotalDays;

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый депозитный счет! Id счета: {this.Id}", this.Sum));
        }

        public override void Put(decimal sum)
        {
            base.Put(sum);
            SumPersent = 0.0m;
            // если бы это был реальный банк, а не симуляция, то и дни шли бы реально
            dateOpen = DateTime.Now; 
            dateToCalculate = dateOpen.AddDays(30);
        }

        public override decimal Withdraw(decimal sum)
        {
            decimal sumWithdraw = base.Withdraw(sum);
            SumPersent = 0.0m;
            // если бы это был реальный банк, а не симуляция, то и дни шли бы реально
            dateOpen = DateTime.Now; 
            dateToCalculate = dateOpen.AddDays(30);
            return sumWithdraw;
        }

        // расчет процентов
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
