﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catalog.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository,IBrandRepository,ITypesRepository
    {
        private readonly ICatalogContext _context;
        public ProductRepository(ICatalogContext context) => _context = context;
        public async Task<Product> CreateProductAsync(Product product)
        {

            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult=await _context.Products.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
        {
            return await _context.Brands.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
        {
            return await _context.Types.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductAsync()
        {

           return await _context.Products.Find(p => true).ToListAsync();
           
        }

        public async Task<Product> GetProductAsync(string id)
        {
           return await _context.Products.Find(p=>p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrandAsync(string name)
        {
            FilterDefinition<Product> filter =Builders<Product>.Filter.Eq(p=>p.Brands.Name,name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var update = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
            return update.IsAcknowledged && update.ModifiedCount>0;
        }
    }
}
