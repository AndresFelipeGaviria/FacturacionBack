using facturacionBack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Models
{
    public class DetailInvoice
    {

        public DetailInvoice()
        {

        }


        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal Precio_Pro { get; set; }
        public Product Product { get; set; }
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }


}
