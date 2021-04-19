using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAddressManager
{
    public class DatabaseContext: DbContext
    {

        public DatabaseContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
    }
}
