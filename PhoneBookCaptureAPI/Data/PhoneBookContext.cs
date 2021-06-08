using Microsoft.EntityFrameworkCore;
using PhoneBookCaptureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookCaptureAPI.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext()
        {
        }
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options)
         : base(options)
        {
        }

        public DbSet<Entry> Entries { get; set; }
        public DbSet<PhoneBook> PhoneBook { get; set; }
    }
}
