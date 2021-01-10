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

        public ActionResult Invoice(int id = 0)
        {
            var vm = new EditInvoiceViewModel
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