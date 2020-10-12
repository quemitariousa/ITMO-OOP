using System;
using System.Collections.Generic;

namespace ShopLaboratory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class Shop
    {
        public string id, name, adress;
        public List<Product> information;
        public Shop(string idOfShop, string nameOfShop, string adressOfShop) {
            id = idOfShop;
            name = nameOfShop;
            adress = adressOfShop;
            information = new List<Product>();
        }
    }

    public class Product
    {
        public string id, name;
        public int price = 0;
        int count = 0;
        
        public Product(string idOfProduct, string nameOfProduct, int priceOfProductm, int countOfProduct) {
            id = idOfProduct;
            name = nameOfProduct;
            price = priceOfProductm;
            count = countOfProduct;
        }
    }

    public class ActionShop { 
        
    
    }




}
