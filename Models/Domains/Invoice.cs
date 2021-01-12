using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class Invoice
    {

        public Invoice()
        {
            InvoicePositions = new Collection<InvoicePossition>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tytuł faktury")]
        public string Title { get; set; }
        [Display(Name = "Wartość")]
        public decimal Value { get; set; }
        [Display(Name = "Sposób płatności")]
        public int MethodOfPaymentId { get; set; }
        [Display(Name = "Termin Płatności")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Data Wystawienia")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Uwagi")]
        public string Comments { get; set; }
        [Display(Name = "Klient")]
        public int ClientId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        [Display(Name = "Sposób płatności")]
        public MethodOfPayment MethodOfPayment { get; set; }
        public Client Client { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<InvoicePossition> InvoicePositions { get; set; }
    }
}