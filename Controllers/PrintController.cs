using InvoiceManager.Models.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceManager.Controllers
{
    [Authorize]
    public class PrintController : Controller
    {
        private InvoiceRepository _invoiceRepository = new InvoiceRepository();
        public ActionResult InvoiceTemplate(int id)
        {


            var userId = User.Identity.GetUserId();
            var invoice = _invoiceRepository.GetInvoice(id,userId);

            return View(invoice);
        }
    }
}