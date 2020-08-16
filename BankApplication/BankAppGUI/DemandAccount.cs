using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, float percentage, AccountType _type) : base(sum, percentage, _type)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый счет до востребования! Id счета: {this.Id}", this.Sum));
        }
    }
}
