using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop_OOP.Models
{
    class ShopManager
    {
        private List<Shop> _shops;

        public ShopManager()
        {
            _shops = new List<Shop>();
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
            return shopWithMinPrice ?? throw new ArgumentException("No this product in any shops");
        }

    }
}
