using InvoiceManager.Models.Domains;
using InvoiceManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var invoices = new List<Invoice>
            {
                new Invoice
                {
                    Id = 1,
                    Title = "FA/01/2021",
                    Value = 5320,
                    CreatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    Client = new Client { Name = "Klient1" }
                },

                new Invoice
                {
                    Id = 2,
                    Title = "FA/02/2021",
                    Value = 120,
                    CreatedDate = DateTime.Now,
                    PaymentDate = DateTime.Now,
                    Client = new Client { Name = "Klient2" }
                }
            };
            
            return View(invoices);
        }

        public ActionResult InvoicePosition(int invoiceId = 0, int invoicePosition = 0)
        {
            EditInvoicePositionViewModel vm = null;
            if (invoicePosition == 0) // dodawanie
            {
                vm = new EditInvoicePositionViewModel
                {
                    Heading = "dodawanie nowej pozycji",

                };
            }
            else
            {

            }

            return View();
        }

        public ActionResult Invoice(int id = 0)
        {

           EditInvoiceViewModel vm = null;

            if(id == 0)
            {
                vm = new EditInvoiceViewModel
                {
                    Clients = new List<Client>
                    {
                        new Client {Id = 1, Name = "Klient1"}
                    },

                    MethodOfPayments = new List<MethodOfPayment>
                    {
                        new MethodOfPayment {Id = 1, Name = "Przelew" }
                    },

                    Heading = "Edycja Faktury 1",
                    Invoice = new Invoice()
                };
            }
            else
            {
                vm = new EditInvoiceViewModel
                {
                    Clients = new List<Client>
                    {
                        new Client { Id = 1, Name = "Klient1" }
                    },

                    MethodOfPayments = new List<MethodOfPayment>
                    {
                        new MethodOfPayment { Id = 1, Name = "Przelew" }
                    },

                    Heading = "Edycja Faktury 1",
                    Invoice = new Invoice()
                    {
                        Id = 1,
                        ClientId = 1,
                        Comments = "komentarz 1",
                        CreatedDate = DateTime.Now,
                        PaymentDate = DateTime.Now,
                        MethodOfPaymentId = 1,
                        Value = 100,
                        Title = "FA/2021/01",
                        InvoicePositions = new List<InvoicePossition>()
                        {
                            new InvoicePossition
                            {
                                Id = 1,
                                InvoiceId = 1,
                                Lp = 1,
                                Product = new Product{Id = 1,Name = "toster"},
                                Quantity = 2,
                                Value = 20,
                                Invoice = new Invoice {Id = 1}
                            },
                            new InvoicePossition
                            {
                                Id = 2,
                                InvoiceId = 1,
                                Lp = 2,
                                Product = new Product{Id = 1,Name = "wiadro"},
                                Quantity = 15,
                                Value = 100,
                                Invoice = new Invoice {Id = 1}
                            }
                        }
                    }
                };
            }
            
            return View(vm); 
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}