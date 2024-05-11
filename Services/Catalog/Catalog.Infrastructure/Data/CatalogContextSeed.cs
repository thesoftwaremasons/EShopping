using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    internal class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {

            bool checkProducts= productCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                var checkData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(checkData);
                if (products != null)
                {
                    productCollection.InsertManyAsync(products);
                }
            }


        }
    }
}
