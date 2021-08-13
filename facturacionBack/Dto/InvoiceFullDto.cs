using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Dto
{
    public class InvoiceFullDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public string NameShopkeeper { get; set; }
        public List<DetailInvoiceDto> DetailInvoiceDto { get; set; }
        public ClientDto ClientDto { get; set; }


    }

    public class ResponseInvoice
    {
        public int InvoiceId { get; set; }
        public int IdClient { get; set; }
        public string NameClient { get; set; }
        public string NameShopkeeper { get; set; }
        public DateTime Date { get; set; }

        public List<DetailInvoiceDto> DetailInvoice { get; set; }

    }
}
