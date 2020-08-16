using System;

namespace BankLib
{
    public interface IAccount
    {
        // Положить деньги на счёт
        void Put(decimal sum);

        // Снять деньги с счёта
        decimal Withdraw(decimal sum);
    }
}
