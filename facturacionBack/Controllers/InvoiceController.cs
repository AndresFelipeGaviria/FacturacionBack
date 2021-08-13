using facturacionBack.Data;
using facturacionBack.Models;
using facturacionBack.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly FacturaContexto _facturaContexto;

        public InvoiceController(FacturaContexto facturaContexto)
        {
            _facturaContexto = facturaContexto;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseInvoice>>> GetAllInvoices()
        {
            //
            var resultInvoices = await _facturaContexto.Invoices.Include("DetailsNavigations").Include("DetailsNavigations.Product").Include("DetailsNavigations").Include("ClientNavigation").ToListAsync();
            var response_invoice = new List<ResponseInvoice>();

            if (resultInvoices == null)
            {
                return BadRequest();
            }

            foreach (var invoice in resultInvoices)
            {


                var itemInvoce = new ResponseInvoice();
                itemInvoce.InvoiceId = invoice.Id;
                itemInvoce.IdClient = invoice.ClientNavigation.Id;
                itemInvoce.NameClient = invoice.ClientNavigation.Name;
                itemInvoce.Date = invoice.Date;

                itemInvoce.DetailInvoice = new List<DetailInvoiceDto>();
                foreach (var detInvoice in invoice.DetailsNavigations)
                {
                    var itemDt = new DetailInvoiceDto();

                    itemDt.Product = new ProductDto
                    {
                        Id = detInvoice.Product.Id,
                        Name = detInvoice.Product.Name,
                        Price = detInvoice.Product.Price
                    };

                    
                    itemDt.Precio_Pro = detInvoice.Precio_Pro;
                    itemDt.Id = detInvoice.Id;
                    itemDt.ProductId = detInvoice.ProductId;
                    itemInvoce.DetailInvoice.Add(itemDt);

                }





                response_invoice.Add(itemInvoce);
            }
            return response_invoice;


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseInvoice>> GetById(int id)
        {
            //
            var invoice = await _facturaContexto.Invoices.Where(x => x.Id == id).Include("DetailsNavigations").Include("DetailsNavigations.Product").Include("DetailsNavigations").Include("ClientNavigation").FirstOrDefaultAsync();

            if (invoice == null)
            {
                return BadRequest();
            }

                var itemInvoce = new ResponseInvoice();
                itemInvoce.InvoiceId = invoice.Id;
                itemInvoce.NameShopkeeper = invoice.NameShopkeeper;
                itemInvoce.IdClient = invoice.ClientNavigation.Id;
                itemInvoce.NameClient = invoice.ClientNavigation.Name;
                itemInvoce.Date = invoice.Date;

                itemInvoce.DetailInvoice = new List<DetailInvoiceDto>();
                foreach (var detInvoice in invoice.DetailsNavigations)
                {
                    var itemDt = new DetailInvoiceDto();

                    itemDt.Product = new ProductDto
                    {
                        Id = detInvoice.Product.Id,
                        Name = detInvoice.Product.Name,
                        Price = detInvoice.Product.Price,
                        
};
                        itemDt.Cantidad = detInvoice.Cantidad;
                        itemDt.Precio_Pro = detInvoice.Precio_Pro;
                        itemDt.Id = detInvoice.Id;
                        itemDt.ProductId = detInvoice.ProductId;
                        itemInvoce.DetailInvoice.Add(itemDt);

                }
           
                    return itemInvoce;


        }

        [HttpPost]
        public async Task<ActionResult<ResponseInvoice>> Post(InvoiceFullDto invoiceFullDto)
        {
            var result = new ResponseInvoice();
            try
            {
                using (var tran = _facturaContexto.Database.BeginTransaction())
                {
                    try
                    {

                        var Invoice = new Invoice();
                        Invoice.ClientId = invoiceFullDto.ClientId;
                        Invoice.NameShopkeeper = invoiceFullDto.NameShopkeeper;
                        Invoice.Date = DateTime.Now;

                        _facturaContexto.Invoices.Add(Invoice);
                        _facturaContexto.SaveChanges();

                        foreach (var item in invoiceFullDto.DetailInvoiceDto)
                        {
                            var listProduct = new DetailInvoice();
                            var prod = _facturaContexto.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                            if (prod == null)
                                return new BadRequestResult();

                            listProduct.ProductId = item.ProductId;
                            listProduct.Precio_Pro = prod.Price;
                            listProduct.Cantidad = item.Cantidad;
                            listProduct.Total = prod.Price * item.Cantidad;
                            listProduct.InvoiceId = Invoice.Id;

                            _facturaContexto.DetailInvoices.Add(listProduct);
                            _facturaContexto.SaveChanges();
                        }


                        await tran.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;

        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<ResponseInvoice>>> Put(InvoiceFullDto invoiceFullDto)
        {
            var result = new ResponseInvoice();
            try
            {
                using (var tran = _facturaContexto.Database.BeginTransaction())
                {
                    try
                    {
                        var rs_invoice = await _facturaContexto.Invoices.Where(x => x.Id == invoiceFullDto.Id).FirstOrDefaultAsync();

                        if (rs_invoice == null)
                        {
                            return BadRequest();
                        }
                        rs_invoice.ClientId = invoiceFullDto.ClientId;
                        rs_invoice.NameShopkeeper = invoiceFullDto.NameShopkeeper;
                        rs_invoice.Date = rs_invoice.Date;

                        _facturaContexto.Entry(rs_invoice).State = EntityState.Modified;
                        _facturaContexto.SaveChanges();

                        foreach (var item in invoiceFullDto.DetailInvoiceDto)
                        {
                            var detailp = _facturaContexto.DetailInvoices.Find(item.ProductId);
                            var listProduct = new DetailInvoiceDto();
                            if (detailp == null)
                            {
                                return BadRequest();
                            }
                            listProduct.ProductId = item.ProductId;
                            listProduct.Precio_Pro = detailp.Precio_Pro;
                            listProduct.Cantidad = item.Cantidad;
                            listProduct.Total = detailp.Precio_Pro * item.Cantidad;

                            _facturaContexto.Entry(listProduct).State = EntityState.Modified;
                            _facturaContexto.SaveChanges();

                        }


                        await tran.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(result);


        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
