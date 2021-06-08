using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBookCapture.Models
{
    public class PhoneBook
    {
        [Key]
        public int PhoneBookID { get; set; }
        public string Name { get; set; }
        public int EntryID { get; set; }
        //public virtual Entry Entry { get; set; }
    }
}
