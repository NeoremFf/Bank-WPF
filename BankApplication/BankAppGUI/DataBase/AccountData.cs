using System;

namespace BankAppGUI
{
    public class AccountData
    { 
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal Sum { get; set; }
        public DateTime? Date { get; set; }
    }
}
