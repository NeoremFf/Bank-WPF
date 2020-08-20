using System;

namespace BankLib
{
    // delegates for events on account
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public delegate void AccountdHandler(Account _account);

    public class AccountOpenedEventArgs
    {
        // current account
        Account account;

        public AccountOpenedEventArgs(Account _account)
        {
            account = _account;
        }
    }

    public class AccountEventArgs
    {
        // message
        public string Message { get; private set; }
        // Sum
        public decimal Sum { get; private set; }

        public AccountEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
