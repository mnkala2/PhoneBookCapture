using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookCaptureAPI.Models
{
    public class Entry
    {
        [Key]
        public int EntryID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
