using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP.Models
{
    class Consigment
    {
        public Product Product { get; }
        public int Count { get; private set; }
        public decimal Price { get; private set; }

        public Consigment(Product product, int count, decimal price)
        {
            Product = product;
            Count = count;
            Price = price;
        }

        public decimal Bye(int count)
        {
            if (count > Count)
            {
                throw new ArgumentException("Not enough products in consigment");
            }

            Count -= count;
            return Price * count;
        }
    }
}
