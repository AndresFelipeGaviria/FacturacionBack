using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Dto
{
    public class InvoiceRequestDto
    {
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string NameShopkeeper { get; set; }
        public List<DetailInvoiceDto> DetailInvoiceDto { get; set; }
        public ClientDto ClientDto { get; set; }
    }
}
