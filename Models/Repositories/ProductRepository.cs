using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Products.ToList();
            }
        }

        public Product GetProduct(int productId)
        {
            using (var context = new ApplicationDbContext())
            {
                // może być First, FirstOrDefault
                // Single jeżeli nie znajdzie lub będzie więcej produktów o danym Id
                // To w tym miejscu zostanie rzucony wyjątek
                return context.Products.Single(x => x.Id == productId);
            }
        }
    }
}