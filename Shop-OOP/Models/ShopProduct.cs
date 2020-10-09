using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP.Models
{
    class ShopProduct
    {
        public Product Product { get; }
        public int Count { get; private set; }
        public decimal Price { get; private set; }

        public ShopProduct(Product product, int count, decimal price)
        {
            Product = product;
            Count = count;
            Price = price;
        }
    }
}
