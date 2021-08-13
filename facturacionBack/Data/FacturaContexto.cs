using facturacionBack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace facturacionBack.Data
{
    public class FacturaContexto : DbContext

    {

        public FacturaContexto()
        {

        }
        public FacturaContexto(DbContextOptions<FacturaContexto> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<DetailInvoice> DetailInvoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity => {
                entity.HasOne(e => e.ClientNavigation)
                 .WithMany(p => p.Invoice)
                 .HasForeignKey(f => f.ClientId)
                 .OnDelete(DeleteBehavior
                 .ClientSetNull)
                 .HasConstraintName("FKInvoice_Client");


                entity.HasMany(e => e.DetailsNavigations)
                 .WithOne(p => p.Invoice)
                 .HasConstraintName("FKInvoice_DetalInvoice");
            });

            modelBuilder.Entity<DetailInvoice>(entity =>
            {
                entity.HasOne(e => e.Product)
                 .WithMany(p => p.DetailInvoice)
                 .HasConstraintName("FKDetailInvoice_Product");



            });
            base.OnModelCreating(modelBuilder);



        }
    }
}
