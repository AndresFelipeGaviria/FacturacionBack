using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Dto
{
    public class DetailInvoiceDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal Precio_Pro { get; set; }
        public ProductDto Product { get; set; }
    }
}
