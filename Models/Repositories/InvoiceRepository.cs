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
                return context.Invoices.Include(x => x.Client )
                    .Where(x => x.UserId == userId)
                    .ToList();
            }
        }

        public Invoice GetInvoice(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Invoices.Single(x => x.Id == id && x.UserId == userId);
            }
        }

        public List<MethodOfPayment> GetMethodOfPayment()
        {
            // TODO: Metoda odczytująca z bazy metody płatności
            using (var context = new ApplicationDbContext())
            {
             
            }
            return new List<MethodOfPayment>();
        }

        public InvoicePossition GetInvoicePosition(int invoicePositionId, string userId)
        {
            // TODO: Medoda odczytująca z bazy pozycję faktury
            using (var context = new ApplicationDbContext())
            {

            }
            return new InvoicePossition();
        }

        public void Update(Invoice invoice)
        {
            // TODO: obsługa aktualizacji faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public void Add(Invoice invoice)
        {
            // TODO: obsługa dodania nowej faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public void AddPosition(InvoicePossition invoicePosition, string userId)
        {
            // TODO: obsługa dodania nowej pozycji do faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public void UpdatePosition(InvoicePossition invoicePosition, string userId)
        {
            // TODO: obsługa aktualizacji pozycji na faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            // TODO: aktualizacja wartości faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public void Delete(int invoiceId, string userId)
        {
            // TODO: usunięcie faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }

        public void DeletePosition(int invoiceId, string userId)
        {
            // TODO: usunięci pozycji faktury
            using (var context = new ApplicationDbContext())
            {

            }
            throw new NotImplementedException();
        }
    }
}