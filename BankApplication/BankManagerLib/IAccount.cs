using System;

namespace BankLib
{
    public interface IAccount
    {
        // Add money to account
        void Put(decimal sum);

        // Withdraw money from account
        decimal Withdraw(decimal sum);
    }
}
