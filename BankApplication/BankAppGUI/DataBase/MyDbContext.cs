using BankLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BankAppGUI
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnectionString")
        { }

        public DbSet<AccountData> AccountsData { get; set; } 
    }
}
