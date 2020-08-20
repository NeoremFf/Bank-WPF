using System;
using System.Collections.Generic;
using System.Text;

namespace BankLib
{
    /// <summary>
    /// Simple account without percentage
    /// </summary>
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, float percentage, AccountType _type, int id = 0) : base(sum, percentage, _type, id)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый счет до востребования! Id счета: {this.Id}", this.Sum));
        }
    }
}
