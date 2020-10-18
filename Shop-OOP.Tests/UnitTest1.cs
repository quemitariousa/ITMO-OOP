using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop_OOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_OOP.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private List<Product> _products;
        private List<Shop> _shops;
        private ShopManager _shopManager;

        [TestInitialize]
        public void Init()
        {
            _products = new List<Product>
            {
                new Product("Board"),
                new Product("Apple"),
                new Product("Border-Collie Toy"),
                new Product("Tihon Toy"),
                new Product("Liquorice"),
                new Product("Fish"),
                new Product("Banana"),
                new Product("Duck"),
                new Product("Laptop"),
                new Product("Flower"),
            };

            _shops = new List<Shop>
            { 
                new Shop("DogsForEveryone", "Everywhere"),
                new Shop("CatsForEveryone", "Zelenogorsk"),
                new Shop("LeraForEveryone", "Korablestroiteley"),
            };

            _shopManager = new ShopManager();
            _shopManager.AddRange(_shops);
        }

        [TestMethod]
        public void Product_availble_after_add_consigment()
        {
            // Arrange
            Shop shop = _shops[0];
            Product product = _products[0];

            // Act
            shop.AddConsignment(product, 10, 10);

            // Assert
            Assert.IsTrue(shop.TryGetMinPrice(product, out decimal price));
        }

        [TestMethod]
        public void Shop_cheapest_product()
        { 
            // Arrange
            Product product = _products[0];
            _shops[0].AddConsignment(product, 10, 11);
            _shops[1].AddConsignment(product, 10, 7);
            _shops[2].AddConsignment(product, 10, 2);
            Shop expectedShop = _shops[2];

            // Act
            Shop actualShop = _shopManager.FindShopWithCheapestProduct(product);

            // Assert
            Assert.IsTrue(expectedShop.Id == actualShop.Id);
        }

        [TestMethod]
        public void Shop_cheapest_product_throw_exception()
        { 
            // Arrange
            Product product = _products[0];

            // Assert
            Assert.ThrowsException<ArgumentException>(() => _shopManager.FindShopWithCheapestProduct(product));
        }
        
        [TestMethod]
        public void Which_product_can_buy_in_special_cash()
        { 
            // Arrange
            Shop shop = _shops[0];
            shop.AddConsignment(_products[2], 10, 10);
            shop.AddConsignment(_products[4], 10, 101);
            shop.AddConsignment(_products[6], 10, 90);

            // Act
            List<(Product, int)> result = shop.CountProductsForSum(100);

            // Assert
            Assert.IsTrue(result.Count() == 2);
            Assert.IsTrue(result.FirstOrDefault(x => x.Item1.Id == _products[2].Id).Item2 == 10);
            Assert.IsTrue(result.FirstOrDefault(x => x.Item1.Id == _products[6].Id).Item2 == 1);
            
        }
        [TestMethod]
        public void Bye_consigment_in_Shop()
        { 
            // Arrange
            Shop shop = _shops[0];
            shop.AddConsignment(_products[5], 4, 10);
            shop.AddConsignment(_products[8], 1, 10);
            List<(Product, int)> shopList = new List<(Product, int)>
            {
                (_products[5], 2),
                (_products[8], 1)            
            };
            decimal expectedSum = 30;

            // Act
            bool result = shop.TryBuyProducts(shopList, out decimal actualSum);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(expectedSum == actualSum);
        }

        [TestMethod]
        public void Find_cheapest_consigment_in_the_shop()
        { 
            // Arrange
            _shops[0].AddConsignment(_products[5], 4, 10);
            _shops[0].AddConsignment(_products[8], 1, 10);

            _shops[1].AddConsignment(_products[5], 4, 10);
            _shops[1].AddConsignment(_products[8], 1, 12);

            List<(Product, int)> shopList = new List<(Product, int)>
            {
                (_products[5], 2),
                (_products[8], 1)            
            };
            Shop expectedShop = _shops[0]; 
            
            // Act
            Shop actualShop = _shopManager.FindShopWithCheapestPurchase(shopList);

            // Assert
            Assert.IsTrue(expectedShop.Id == actualShop.Id);
        }
    }
}
