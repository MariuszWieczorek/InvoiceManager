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
            //TODO: Pobieranie Listy produktów z bazy
            return new List<Product>();
        }

        public Product GetProduct(int productId)
        {
            // TODO: Pobranie produktu z bazy
            return new Product(); 
        }
    }
}