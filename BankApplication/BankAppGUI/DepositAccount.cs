using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum, float percentage, AccountType _type) : base(sum, percentage, _type)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый депозитный счет! Id счета: {this.Id}", this.Sum));
        }

        public override void Calculate(DateTime currentDate)
        {
            DateTime addDays = dateOpen;
            if (currentDate >= addDays.AddDays(30))
                base.Calculate(currentDate);
        }
    }
}
