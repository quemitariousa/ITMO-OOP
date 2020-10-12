using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Shop_OOP.Models
{
    public class Shop
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        private List<ShopProduct> _storage;

        public Shop(string name, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            _storage = new List<ShopProduct>();
        }

        public void AddProductsConsignment(Product product, int count, decimal price)
        {
            _storage.Add(new ShopProduct(product, count, price));


        }

        public List<(Product, int)> CountProductsByPrice(decimal sum)
        {
            //return _storage
            //    .Select(x => x.Product)
            //    .Distinct()
            //    .Select(x => (x, CountProductByPrice(x, sum)))
            //    .ToList();


        }

        private int CountProductByPrice(Product product, decimal sum)
        {
            return 0;
        }

        public bool TryGetMinPrice(Product product, out decimal price)
        {
            List<decimal> prices = _storage
                .Where(x => x.Product.Id == product.Id)
                .Select(x => x.Price)
                .ToList();

            if (prices.Count() == 0)
            {
                price = -1;
                return false;
            }
            else
            {
                price = prices.Min();
                return true;
            }

        }


    }
}
