using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Models
{
    public class Client
    {
        public Client()
        {
            Invoice = new HashSet<Invoice>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Telephone { get; set; }
        public string Address { get; set; }
        public ICollection<Invoice> Invoice { get; set; }

    }
}
