using facturacionBack.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Models
{
    public class Invoice
    {

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string NameShopkeeper { get; set; }
        public List<DetailInvoice> DetailsNavigations { get; set; }
        public Client ClientNavigation { get; set; }

    }



}
