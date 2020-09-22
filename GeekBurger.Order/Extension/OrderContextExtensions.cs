using GeekBurger.Order.Model;
using GeekBurger.Order.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GeekBurger.Order.Extension
{
    public static class OrderContextExtensions
    {

        public static void Seed(this OrderDbContext dbContext)
        {
            if (dbContext.Payments.Any() ||
                dbContext.Orders.Any() ||
                dbContext.OrderChangedEvents.Any() ||
                dbContext.Stores.Any())
                return;

            dbContext.Stores.AddRange(
                new List<Model.Store> {
                    new Model.Store {
                        Name = "Paulista",
                        StoreId = new Guid("8048e9ec-80fe-4bad-bc2a-e4f4a75c834e")
                    },
                    new Model.Store {
                        Name = "Morumbi",
                        StoreId = new Guid("8d618778-85d7-411e-878b-846a8eef30c0")
                    }
                }
            );

            var productsTxt = File.ReadAllText("products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsTxt);
            dbContext.Products.AddRange(products);

            dbContext.SaveChanges();

        }

    }
}
