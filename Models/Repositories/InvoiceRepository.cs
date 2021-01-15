using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class InvoiceRepository
    {
        public List<Invoice> GetInvoices(string userId)
        {
            // TODO: Metoda Odczytująca Faktury z Bazy 
            return new List<Invoice>();
        }

        public Invoice GetInvoice(int id, string userId)
        {
            // TODO: Metoda Odczytująca Pojedynczą Fakturę z Bazy 
            return new Invoice();
        }

        public List<MethodOfPayment> GetMethodOfPayment()
        {   
            // TODO: Metoda odczytująca z bazy metody płatności
            return new List<MethodOfPayment>();
        }

        public InvoicePossition GetInvoicePosition(int invoicePositionId, string userId)
        {
            // TODO: Medoda odczytująca z bazy pozycję faktury
            return new InvoicePossition();
        }

        public void Update(Invoice invoice)
        {
            // TODO: obsługa aktualizacji faktury
            throw new NotImplementedException();
        }

        public void Add(Invoice invoice)
        {
            // TODO: obsługa dodania nowej faktury
            throw new NotImplementedException();
        }

        public void AddPosition(InvoicePossition invoicePosition, string userId)
        {
            // TODO: obsługa dodania nowej pozycji do faktury
            throw new NotImplementedException();
        }

        public void UpdatePosition(InvoicePossition invoicePosition, string userId)
        {
            // TODO: obsługa aktualizacji pozycji na faktury
            throw new NotImplementedException();
        }

        public decimal UpdateInvoiceValue(int invoiceId, string userId)
        {
            // TODO: aktualizacja wartości faktury
            throw new NotImplementedException();
        }

        public void Delete(int invoiceId, string userId)
        {
            // TODO: usunięcie faktury
            throw new NotImplementedException();
        }

        public void DeletePosition(int invoiceId, string userId)
        {
            // TODO: usunięci pozycji faktury
            throw new NotImplementedException();
        }
    }
}