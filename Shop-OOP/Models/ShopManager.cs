using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop_OOP.Models
{
    public class ShopManager
    {
        private List<Shop> _shops;

        public ShopManager()
        {
            _shops = new List<Shop>();
        }

        public void Add(Shop shop)
        {
            _shops.Add(shop);
        }

        public void AddRange(List<Shop> shops)
        {
            _shops.AddRange(shops);
        }

        public Shop FindShopWithCheapestProduct(Product product)
        {
            decimal minPrice = decimal.MaxValue;
            Shop shopWithMinPrice = null;

            foreach (Shop shop in _shops)
            {
                if (shop.TryGetMinPrice(product, out decimal price))
                {
                    if (price < minPrice)
                    {
                        minPrice = price;
                        shopWithMinPrice = shop;
                    }
                }
            }
            return shopWithMinPrice ?? throw new ArgumentException("No this product in any shop");
        }

        public Shop FindShopWithCheapestPurchase(List<(Product, int)> shoppingList)
        {
            decimal minSum = decimal.MaxValue;
            Shop shopWithMinSum = null;

            foreach(var shop in _shops)
            {
                if (shop.TryCountPrice(shoppingList, out decimal sum))
                {
                    if (sum < minSum)
                    {
                        minSum = sum;
                        shopWithMinSum = shop;
                    }
                }
            }

            return shopWithMinSum ?? throw new ArgumentException("Unvailable to buy products in any shop");
        }

    }
}
