using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // wyświetlamy w widoku Client.Name dlatego musimy
                // załączyć również dane o kliencie
                return context.Invoices.Include(x => x.Client )
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public Invoice GetInvoice(int id, string userId)
        {
            //TODO: Tu powinno być ID faktury a nie pozycji
            using (var context = new ApplicationDbContext())
            {
                // chcemy załączyć:
                // informacje o pozycjach
                // informacje o Produktach
                return context.Invoices
                    .Include(x => x.InvoicePositions)
                    .Include(x => x.InvoicePositions.Select(y => y.Product))
                    .Include(x => x.MethodOfPayment)
                    .Include(x => x.User)
                    .Include(x => x.User.Address)
                    .Include(x => x.Client)
                    .Include(x => x.Client.Address)
                    .Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public List<MethodOfPayment> GetMethodOfPayment()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.MethodOfPayments.ToList(); 
            }
        }

        public InvoicePossition GetInvoicePosition(int invoicePositionId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // Sprawdzamy, czy faktura należy od użytkownika
                return context.InvoicePossitions
                    .Include(x => x.Product)
                    .Include(x => x.Invoice)
                    .Single(x => x.Id == invoicePositionId && x.Invoice.UserId == userId);
            }
         }

        public void Update(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pojedynczy rekord z fakturą do aktualizacji
                var invoiceToUpdate = context.Invoices
                    .Single(x => x.Id == invoice.Id && x.UserId == invoice.UserId);

                // dokonujemu zmian  
                invoiceToUpdate.Client = invoice.Client;
                invoiceToUpdate.Comments = invoice.Comments;
                invoiceToUpdate.MethodOfPaymentId = invoice.MethodOfPaymentId;
                invoiceToUpdate.PaymentDate = invoice.PaymentDate;
                invoiceToUpdate.Title = invoice.Title;

                // zapisujemy zmiany 
                context.SaveChanges();
            }
        }

        public void Add(Invoice invoice)
        {
            using (var context = new ApplicationDbContext())
            {
                invoice.CreatedDate = DateTime.Now;
                invoice.PaymentDate = DateTime.Now;
                context.Invoices.Add(invoice);
                context.SaveChanges();
            }
        }

        public void AddPosition(InvoicePossition invoicePosition, string userId)
        {   
            // na początku musimy się upewnić, czy użytkownik dodaje pozycje do swojej faktury
            // czyli sprawdzamy czy istnieje faktura o id takim jak id dodawnej pozycji
            // i czy ta faktura przynależy do użytkownika
            using (var context = new ApplicationDbContext())
            {
                var invoiceToUpdate = context.Invoices
                   .Single(x => 
                   x.Id == invoicePosition.InvoiceId && 
                   x.UserId == userId );

                // jeżeli nie ma takiego rekordu to zostanie rzucony wyjątek

                context.InvoicePossitions.Add(invoicePosition);
                context.SaveChanges();
            }
        }

        public void UpdatePosition(InvoicePossition invoicePosition, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pozycje z faktury
                var invoicePositionToUpdate = context.InvoicePossitions
                    .Include(x => x.Product)
                    .Include(x => x.Invoice)
                    .Single(x =>
                    x.Id == invoicePosition.Id &&
                    x.Invoice.UserId == userId);

                var productInInvoicePossition = context.Products
                    .Single(x =>
                    x.Id == invoicePosition.ProductId);

                invoicePositionToUpdate.Lp = invoicePosition.Lp;
                invoicePositionToUpdate.ProductId = invoicePosition.ProductId;
                invoicePositionToUpdate.Quantity = invoicePosition.Quantity;
                invoicePositionToUpdate.Value = invoicePosition.Quantity * productInInvoicePossition.Value;

                context.SaveChanges();

            }
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToUpdate = context.Invoices
                  .Include( x => x.InvoicePositions)  
                  .Single(x =>
                  x.Id == invoiceId &&
                  x.UserId == userId);

                invoiceToUpdate.Value = invoiceToUpdate.InvoicePositions.Sum(x => x.Value);

                context.SaveChanges();

                return invoiceToUpdate.Value;
            }
        }

        public void Delete(int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoiceToDelete = context.Invoices
                 .Single(x =>
                 x.Id == invoiceId &&
                 x.UserId == userId);

                context.Invoices.Remove(invoiceToDelete);
                context.SaveChanges();
            }
        }

        public void DeletePosition(int invoicePositionId,int invoiceId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var invoicePositionToDelete = context.InvoicePossitions
                    .Include(x => x.Invoice)
                    .Single(x =>
                    x.Id == invoicePositionId &&
                    x.InvoiceId == invoiceId &&
                    x.Invoice.UserId == userId);

                context.InvoicePossitions.Remove(invoicePositionToDelete);
                context.SaveChanges();
            }
        }
    }
}