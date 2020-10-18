using Shop_OOP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_OOP.Models
{
    public class Shop
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        private List<Consigment> _storage;

        public Shop(string name, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            _storage = new List<Consigment>();
        }

        public void AddProductsConsignment(Product product, int count, decimal price)
        {
            _storage.Add(new Consigment(product, count, price));
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

        public List<(Product, int)> ProductsCanIBuyInTheShop(decimal sum)
        {
            return _storage
                .Select(x => x.Product)
                .Distinct(new ProductComparer())
                .Select(x => (x, HowMuchCanBought(x, sum)))
                .ToList();
        }

        private int HowMuchCanBought(Product product, decimal sum)
        {
            List<Consigment> consigments = _storage
                .Where(x => x.Product.Id == product.Id)
                .OrderBy(x => x.Price)
                .ToList();

            int countOfProduct = 0;
            foreach (var consigment in consigments)
            {
                decimal fullConsigmentPrice = consigment.Price * consigment.Count;
                if (fullConsigmentPrice <= sum)
                {
                    countOfProduct += consigment.Count;
                    sum -= fullConsigmentPrice;
                }
                else
                {
                    countOfProduct += Convert.ToInt32(sum / consigment.Price);
                    break;
                }
            }

            return countOfProduct;

        }

      //  public bool TryBuyProducts(List<(Product, int)> shoppingList, out decimal sum)
      //  {
      //
       // }

        public bool TryBuyProduct(Product product, int count, out decimal sum)
        {
            List<Consigment> consigments = _storage
               .Where(x => x.Product.Id == product.Id)
               .OrderBy(x => x.Price)
               .ToList();

            sum = 0;

            int countInStorage = consigments
                .Select(x => x.Count)
                .Sum();
            if (count > countInStorage)
            {
                return false;
            }

            foreach (var consigment in consigments)
            {
                if (consigment.Count < count)
                {
                    count -= consigment.Count;
                    sum += consigment.Bye(consigment.Count);
                }
                else
                {
                    sum += consigment.Bye(count);
                    break;
                }
            }

            return true;
        }
    }
}
