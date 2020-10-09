using System;
using System.Collections.Generic;
using System.Text;

namespace Shop_OOP.Models
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }

        public Product(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
