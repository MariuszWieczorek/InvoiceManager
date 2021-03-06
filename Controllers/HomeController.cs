﻿using InvoiceManager.Models.Domains;
using InvoiceManager.Models.Repositories;
using InvoiceManager.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        private ClientRepository _clientRepository = new ClientRepository();
        private ProductRepository _productRepository = new ProductRepository();

        [NonAction]
        public int Testy()
        {
            return 1;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var invoices = _invoiceRepository.GetInvoices(userId);
            return View(invoices);
        }

        #region Invoice 
        
        public ActionResult Invoice(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            var invoice = id == 0 ?
                GetNewInvoice(userId) :
                _invoiceRepository.GetInvoice(id, userId);

            var vm = PrepareInvoiceVm(invoice, userId); 

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Invoice(Invoice invoice)
        {
            var userId = User.Identity.GetUserId();
            invoice.UserId = userId;

            //System.IO.File.AppendAllText(@"d:\plik.txt",$"{invoice.Title} {invoice.PaymentDate} \n");

            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoiceVm(invoice, userId);
                return View("Invoice", vm);
            } 

            if (invoice.Id == 0)
                _invoiceRepository.Add(invoice);
            else
                _invoiceRepository.Update(invoice);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteInvoice(int invoiceId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.Delete(invoiceId, userId);
            }
            catch (Exception exception)
            {
                // TODO: logowanie do pliku niepowodzenie usunięcia faktury
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        private EditInvoiceViewModel PrepareInvoiceVm(Invoice invoice, string userId)
        {
            return new EditInvoiceViewModel
            {
                Invoice = invoice,
                Heading = invoice.Id == 0 ? "Dodawanie Nowej Faktury" : "Edycja",
                Clients = _clientRepository.GetClients(userId),
                MethodOfPayments = _invoiceRepository.GetMethodOfPayment()
            };
        }

        private Invoice GetNewInvoice(string userId)
        {
            return new Invoice
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentDate = DateTime.Now.AddDays(7)
            };
        }

        #endregion

        #region InvoicePossition

        [HttpGet]
        public ActionResult InvoicePosition(int invoiceId = 0, int invoicePositionId = 0)
        {
            var userId = User.Identity.GetUserId();
            var invoicePosition = invoicePositionId == 0 ?
                GetNewInvoicePosition(invoiceId, invoicePositionId) :
                _invoiceRepository.GetInvoicePosition(invoicePositionId,userId);


            var vm = PrepareInvoicePositionVm(invoicePosition);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InvoicePosition(InvoicePossition invoicePosition)
        {
            var userId = User.Identity.GetUserId();
            // zweryfikujemy, czy uzytkownik próbuje zaktualizować swoją fakturę
            // dodajemy lub aktualizujemy pozycję faktury
            // wyliczymy wartość pozycji
            // wyliczymy i zaktualizujemy wrtość faktury


            if (!ModelState.IsValid)
            {
                var vm = PrepareInvoicePositionVm(invoicePosition);
                return View("InvoicePosition", vm);
            }

            var product = _productRepository.GetProduct(invoicePosition.ProductId);
            invoicePosition.Value = product.Value * invoicePosition.Quantity;

            if (invoicePosition.Id == 0)
                _invoiceRepository.AddPosition(invoicePosition, userId);
            else
                _invoiceRepository.UpdatePosition(invoicePosition, userId);

            _invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);

            return RedirectToAction("Invoice", new { id = invoicePosition.InvoiceId });
            
        }

        [HttpPost]
        public ActionResult DeleteInvoicePosition(int positionId, int invoiceId)
        {
            var invoiceValue = 0m;

            try
            {
                var userId = User.Identity.GetUserId();
                _invoiceRepository.DeletePosition(positionId,invoiceId, userId);
                // musimy jeszcze zaktualizować wartość faktury
                invoiceValue = _invoiceRepository.UpdateInvoiceValue(invoiceId, userId);
            }
            catch (Exception exception)
            {
                // TODO: logowanie do pliku niepowodzenie usunięcia faktury
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true, InvoiceValue = invoiceValue });
        }

        private EditInvoicePositionViewModel PrepareInvoicePositionVm(InvoicePossition invoicePosition)
        {
            return new EditInvoicePositionViewModel
            {
                InvoicePosition = invoicePosition,
                Heading = invoicePosition.Id == 0 ?
                "Dodawanie nowej pozycji" :
                "Edycja Pozycja",
                Products = _productRepository.GetProducts()
            };
        }

        private InvoicePossition GetNewInvoicePosition(int invoiceId, int invoicePositionId)
        {
            return new InvoicePossition
            {
                InvoiceId = invoiceId,
                Id = invoicePositionId
            };
        }

        #endregion

        public static int number = 0; 
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        public ActionResult Contact()
        {
            number++;
            ViewBag.Message = "Your contact page." + number;

            return View();
        }


        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var valSession = GetSession();
            valSession++;
            UpdateSession(valSession);
            ViewBag.ValSession = valSession;

            var valCookie = GetCookie();
            valCookie++;
            UpdateCookie(valCookie);
            ViewBag.ValCookie = valCookie;

            var valCache = GetCache();
            valCache++;
            UpdateCache(valCache);
            ViewBag.ValCache = valCache;


            return View();
        }

        private void UpdateSession(int i)
        {
            Session["klucz"] = i;
        }

        private int GetSession()
        {
            if (Session["klucz"] != null)
                return (int)Session["klucz"];
            return 0;
        }

        private void UpdateCookie(int i)
        {
            var cookie = new HttpCookie("klucz",i.ToString());
            cookie.Expires = DateTime.Now.AddDays(365);
            Response.SetCookie(cookie);
        }

        private int GetCookie()
        {
            if (Request.Cookies["klucz"] != null)
                return int.Parse(Request.Cookies["klucz"].Value);
            return 0;
        }


        private void UpdateCache(int i)
        {
               HttpRuntime.Cache["klucz"] = i;
        }

        private int GetCache()
        {
            if (HttpRuntime.Cache["klucz"] != null)
                return (int)HttpRuntime.Cache["klucz"];
            return 0;
        }


    }
}