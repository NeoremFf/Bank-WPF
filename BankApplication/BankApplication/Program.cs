using System;
using System.Data;
using BankLib;

namespace BankApplication.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("GoldBank");
            DateTime date = new DateTime();
            date = DateTime.Today;
            Console.WriteLine($"Добро пожаловать в {bank.Name}.\n");
            bool alive = true;
            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                // Работа с датой
                Console.WriteLine($"Текущий день: {date:d}");
                // Проверка 30-ти дневного периода
                foreach (var acc in bank)
                {
                    Account accaunt = (Account)acc;
                    if (CheckThirtyDayPeriod(accaunt, date))
                    {
                        Console.WriteLine($"Аккаунт {accaunt.Id}: Тридцатидневный период завершен.");
                        accaunt.Calculate(date);
                    }
                }
                //
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Открыть счет \t 3. Вывести средства  \t 5. Настройка даты \t 7. Выйти из программы");
                Console.WriteLine("2. Закрыть счет \t 4. Пополнить счёт \t 6. Список всех аккаунтов");
                Console.Write("Введите номер пункта: ");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            CloseAccount(bank);
                            break;
                        case 3:
                            Withdraw(bank);
                            break;
                        case 4:
                            Put(bank);
                            break;
                        case 5:
                            SetDate(ref date);
                            break;
                        case 6:
                            ShowAllAccounts(bank);
                            break;
                        case 7:
                            alive = false;
                            continue;
                    }
                }
                catch (Exception ex)
                {
                    // выводим сообщение об ошибке
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
                finally
                {
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static bool CheckThirtyDayPeriod(Account bankAcc, DateTime currentDateInBank)
        {
            DateTime addDaysToDate = bankAcc.dateOpen;
            return currentDateInBank >= addDaysToDate.AddDays(30);
        }

        private static void SetDate(ref DateTime date)
        {
            Console.Write("1. Добавить N дней\n2. Добавить 31 дней (один период)\nВведите номер пункта: ");
            int command = Convert.ToInt32(Console.ReadLine());
            switch (command)
            {
                case 1:
                    Console.Write("Введите количество дней: ");
                    command = Convert.ToInt32(Console.ReadLine());
                    date.AddDays(command);
                    break;
                case 2:
                    command = 31;
                    break;
            }
            date = date.AddDays((double)command);
            Console.WriteLine($"К дате успешно добавленно {command} дней");
        }

        private static void ShowAllAccounts(Bank<Account> bank)
        {
            if (bank.HasAccounts())
                Console.Write(bank);
            else
                Console.WriteLine("В банке нет аккаунтов.");
        }

        public static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета:\n1. До востребования\n2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType,
                sum,
                AddSumHandler,  // обработчик добавления средств на счет
                WithdrawSumHandler, // обработчик вывода средств
                (o, e) => Console.WriteLine(e.Message), // обработчик начислений процентов в виде лямбда-выражения
                CloseAccountHandler, // обработчик закрытия счета
                OpenAccountHandler); // обработчик открытия счета
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Укажите сумму для вывода со счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.Write("Укажите сумму, чтобы положить на счет: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Введите Id счета: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bank.Put(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Введите id счета, который надо закрыть:");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        // обработчики событий класса Account
        // обработчик открытия счета
        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик добавления денег на счет
        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        // обработчик вывода средств
        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Идем тратить деньги");
        }
        // обработчик закрытия счета
        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}