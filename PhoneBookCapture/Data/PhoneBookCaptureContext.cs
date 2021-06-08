using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneBookCapture.Models;

namespace PhoneBookCapture.Data
{
    public class PhoneBookCaptureContext : DbContext
    {
        public PhoneBookCaptureContext (DbContextOptions<PhoneBookCaptureContext> options)
            : base(options)
        {
        }

        public DbSet<PhoneBookCapture.Models.Entry> Entry { get; set; }

        public DbSet<PhoneBookCapture.Models.PhoneBook> PhoneBook { get; set; }
    }
}
