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

        public void AddConsignment(Product product, int count, decimal price)
        {
            _storage.Add(new Consigment(product, count, price));
        }

        public bool TryGetMinPrice(Product product, out decimal price)
        {
            List<decimal> prices = _storage
                .Where(x => x.Product.Id == product.Id && x.Count > 0)
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

        public List<(Product, int)> CountProductsForSum(decimal sum)
        {
            return _storage
                .Select(x => x.Product)
                .Distinct(new ProductComparer())
                .Select(x => (x, HowMuchCanBought(x, sum)))
                .Where(x => x.Item2 > 0)
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
                    countOfProduct += Decimal.ToInt32(sum / consigment.Price);
                    break;
                }
            }

            return countOfProduct;
        }

        public bool TryBuyProducts(List<(Product, int)> shoppingList, out decimal sum)
        {
            sum = 0;

            bool isAvailable = shoppingList
                .All(x => CheckBuyOpportunity(x.Item1, x.Item2));
            if (!isAvailable)
                return false;

            sum = shoppingList
                .Select(x => BuyProduct(x.Item1, x.Item2))
                .Sum();

            return true;
        }

        public bool TryCountPrice(List<(Product, int)> shoppingList, out decimal sum)
        { 
            sum = 0;

            bool isAvailable = shoppingList
                .All(x => CheckBuyOpportunity(x.Item1, x.Item2));
            if (!isAvailable)
                return false;

            sum = shoppingList
                .Select(x => CountPrice(x.Item1, x.Item2))
                .Sum();

            return true;
        }

        private decimal BuyProduct(Product product, int count)
        {
            if (!CheckBuyOpportunity(product, count))
                throw new ArgumentException("Dont have enough product in shop");

            decimal sum = 0;

            List<Consigment> consigments = _storage
               .Where(x => x.Product.Id == product.Id)
               .OrderBy(x => x.Price)
               .ToList();

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

            return sum;
        }

        private bool CheckBuyOpportunity(Product product, int count)
        {
            int countInStorage = _storage
               .Where(x => x.Product.Id == product.Id)
               .Select(x => x.Count)
               .Sum();

            return countInStorage >= count;
        }

        private decimal CountPrice(Product product, int count)
        {
            if (!CheckBuyOpportunity(product, count))
                throw new ArgumentException();

            decimal sum = 0;

            List<Consigment> consigments = _storage
               .Where(x => x.Product.Id == product.Id)
               .OrderBy(x => x.Price)
               .ToList();

            foreach (var consigment in consigments)
            {
                if (consigment.Count < count)
                {
                    count -= consigment.Count;
                    sum += consigment.Count * consigment.Price;
                }
                else
                {
                    sum += count * consigment.Price;
                    break;
                }
            }

            return sum;
        }
    }
}
