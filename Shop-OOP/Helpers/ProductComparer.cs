using System;
using System.Collections.Generic;
using System.Text;
using Shop_OOP.Models;

namespace Shop_OOP.Helpers
{
    class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Product obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
